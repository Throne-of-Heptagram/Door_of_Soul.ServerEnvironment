using Door_of_Soul.Communication.SceneServer;
using Door_of_Soul.Core;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.IO;
using System.Threading;

namespace Door_of_Soul.SceneServer.PhotonServer
{
    public class SceneServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public static ServerPeer ServerPeer { get; private set; }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ScenePeer(initRequest);
        }

        protected override void Setup()
        {
            SetupLog();
            SetupConfiguration();
            CommunicationService.Initial(new SceneServerCommunicationService());
            SetupServerConnection();
            Log.Info("SceneServerApplication Setup.");
        }

        protected override void TearDown()
        {
            Log.Info("Server TearDown.");
            DeviceFactory.Instance.OnSubjectAdded -= LogDeviceConnected;
            DeviceFactory.Instance.OnSubjectRemoved -= LogDeviceDisconnected;
        }

        private void SetupServerConnection()
        {
            ServerPeer = new ServerPeer(this);
            Thread.Sleep(ServerEnvironmentConfiguration.Instance.SetupConnectionDelay);
            if (!CommunicationService.Instance.ConnectHexagrameEntranceServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerAddress,
                port: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.HexagramEntranceServerApplicationName))
            {
                throw new NotSupportedException("ConnectHexagrameEntranceServer Failed");
            }
        }

        private void SetupLog()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }

            DeviceFactory.Instance.OnSubjectAdded += LogDeviceConnected;
            DeviceFactory.Instance.OnSubjectRemoved += LogDeviceDisconnected;
        }

        private void SetupConfiguration()
        {
            ServerEnvironmentConfiguration serverEnvironmentConfiguration;
            if (GenericConfigurationLoader<ServerEnvironmentConfiguration>.Load(Path.Combine(ApplicationPath, "config", "ServerEnvironment.config"), out serverEnvironmentConfiguration))
            {
                ServerEnvironmentConfiguration.Initial(serverEnvironmentConfiguration);
            }
            else
            {
                throw new FileLoadException("ServerEnvironmentConfiguration Load Failed");
            }
        }

        private void LogDeviceDisconnected(TerminalDevice device)
        {
            Log.Info($"Device: {device} disconnected");
        }

        private void LogDeviceConnected(TerminalDevice device)
        {
            Log.Info($"Device: {device} connected");
        }
    }
}
