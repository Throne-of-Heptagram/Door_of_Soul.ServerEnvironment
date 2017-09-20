using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Core;
using Door_of_Soul.Database.Connection;
using Door_of_Soul.Database.MariaDb.Connection;
using Door_of_Soul.Database.MariaDb.Repository;
using Door_of_Soul.Database.Repository;
using Door_of_Soul.Server;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MySql.Data.MySqlClient;
using Photon.SocketServer;
using System.IO;
using System.Threading;

namespace Door_of_Soul.ProxyServer.PhotonServer
{
    class ProxyServerEnvironment : ServerEnvironment.ServerEnvironment
    {
        public static ServerPeer ServerPeer { get; private set; }

        public override bool SetupCommunication(out string errorMessage)
        {
            CommunicationService.Initialize(new ProxyServerCommunicationService());

            ServerPeer = new ServerPeer(ApplicationBase.Instance);
            Thread.Sleep(ServerEnvironmentConfiguration.Instance.SetupConnectionDelay);
            if (!CommunicationService.Instance.ConnectHexagrameEntranceServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerAddress,
                port: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerApplicationName))
            {
                errorMessage = "ConnectHexagrameEntranceServer Failed";
                return false;
            }

            errorMessage = "";
            return true;
        }

        public override bool SetupConfiguration(out string errorMessage)
        {
            ServerEnvironmentConfiguration serverEnvironmentConfiguration;
            if (GenericConfigurationLoader<ServerEnvironmentConfiguration>.Load(Path.Combine(ApplicationBase.Instance.ApplicationPath, "config", "ServerEnvironment.config"), out serverEnvironmentConfiguration))
            {
                ServerEnvironmentConfiguration.Initialize(serverEnvironmentConfiguration);
                errorMessage = "";
                return true;
            }
            else
            {
                errorMessage = "ServerEnvironmentConfiguration Load Failed";
                return false;
            }
        }

        public override bool SetupLog(out string errorMessage)
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationBase.Instance.ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(ApplicationBase.Instance.BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                errorMessage = "Setup Log Failed";
                return false;
            }

            DeviceFactory.Instance.OnSubjectAdded += LogDeviceConnected;
            DeviceFactory.Instance.OnSubjectRemoved += LogDeviceDisconnected;

            errorMessage = "";
            return true;
        }

        public override bool SetupServer(out string errorMessage)
        {
            ServerInitializer.Initialize(new ProxyServerInitializer());
            return ServerInitializer.Instance.Initialize(out errorMessage);
        }

        public override void TearDown()
        {
            DeviceFactory.Instance.OnSubjectAdded -= LogDeviceConnected;
            DeviceFactory.Instance.OnSubjectRemoved -= LogDeviceDisconnected;
        }

        private void LogDeviceDisconnected(TerminalDevice device)
        {
            ProxyServerApplication.Log.Info($"Device: {device} disconnected");
        }

        private void LogDeviceConnected(TerminalDevice device)
        {
            ProxyServerApplication.Log.Info($"Device: {device} connected");
        }

        public override bool SetupDatabase(out string errorMessage)
        {
            ThroneDataConnection<MySqlConnection>.Initialize(new MariaDbThroneDataConnection());

            AnswerRepository.Initialize(new MariaDbAnswerRepository());

            return ThroneDataConnection<MySqlConnection>.Instance.Connect(
                serverAddress: ServerEnvironmentConfiguration.Instance.DatabaseServerAddress,
                port: ServerEnvironmentConfiguration.Instance.DatabasePort,
                username: ServerEnvironmentConfiguration.Instance.DatabaseUsername,
                password: ServerEnvironmentConfiguration.Instance.DatabasePassword,
                databasePrefix: ServerEnvironmentConfiguration.Instance.DatabasePrefix,
                charset: ServerEnvironmentConfiguration.Instance.DatabaseCharset,
                errorMessage: out errorMessage);
        }
    }
}
