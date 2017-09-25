using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Core;
using Door_of_Soul.Database.Connection;
using Door_of_Soul.Database.MariaDb.Connection;
using Door_of_Soul.Database.MariaDb.Relation.Throne;
using Door_of_Soul.Database.MariaDb.Repository.Throne;
using Door_of_Soul.Database.Relation.Throne;
using Door_of_Soul.Database.Repository.Throne;
using Door_of_Soul.Server;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MySql.Data.MySqlClient;
using Photon.SocketServer;
using System.IO;

namespace Door_of_Soul.HexagramThroneServer.PhotonServer
{
    class HexagramThroneServerEnvironment : ServerEnvironment.ServerEnvironment
    {
        public static CentralPeer CentralPeer { get; private set; }
        public static bool ConnectHexagrameCentralServer(out string errorMessage)
        {
            if (CentralCommunicationService.Instance.ConnectHexagrameCentralServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.HexagramCentralServerAddress,
                port: ServerEnvironmentConfiguration.Instance.HexagramCentralServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.HexagramCentralServerApplicationName))
            {
                errorMessage = "";
                return true;
            }
            else
            {
                errorMessage = "ConnectHexagrameCentralServer Failed";
                return false;
            }
        }

        public override bool SetupCommunication(out string errorMessage)
        {
            CentralCommunicationService.Initialize(new HexagramCentralCommunicationService());
            CentralPeer = new CentralPeer(ApplicationBase.Instance);
            return ConnectHexagrameCentralServer(out errorMessage);
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

            ThroneHexagramEntranceFactory.Instance.OnSubjectAdded += LogEntranceConnected;
            ThroneHexagramEntranceFactory.Instance.OnSubjectRemoved += LogEntranceDisconnected;

            errorMessage = "";
            return true;
        }

        public override bool SetupServer(out string errorMessage)
        {
            ServerInitializer.Initialize(new HexagramThroneServerInitializer());
            return ServerInitializer.Instance.Initialize(out errorMessage);
        }

        public override void TearDown()
        {
            ThroneHexagramEntranceFactory.Instance.OnSubjectAdded -= LogEntranceConnected;
            ThroneHexagramEntranceFactory.Instance.OnSubjectRemoved -= LogEntranceDisconnected;
        }

        private void LogEntranceDisconnected(ThroneHexagramEntrance entrance)
        {
            HexagramThroneServerApplication.Log.Info($"Entrance: {entrance} disconnected");
        }

        private void LogEntranceConnected(ThroneHexagramEntrance entrance)
        {
            HexagramThroneServerApplication.Log.Info($"Entrance: {entrance} connected");
        }

        public override bool SetupDatabase(out string errorMessage)
        {
            ThroneDataConnection<MySqlConnection>.Initialize(new MariaDbThroneDataConnection());

            AnswerRepository.Initialize(new MariaDbAnswerRepository());
            TrinityRelation.Initialize(new MariaDbTrinityRelation());

            if(!ThroneDataConnection<MySqlConnection>.Instance.Connect(
                serverAddress: ServerEnvironmentConfiguration.Instance.DatabaseServerAddress,
                port: ServerEnvironmentConfiguration.Instance.DatabasePort,
                username: ServerEnvironmentConfiguration.Instance.DatabaseUsername,
                password: ServerEnvironmentConfiguration.Instance.DatabasePassword,
                databasePrefix: ServerEnvironmentConfiguration.Instance.DatabasePrefix,
                charset: ServerEnvironmentConfiguration.Instance.DatabaseCharset,
                errorMessage: out errorMessage))
            {
                return false;
            }
            return true;
        }
    }
}
