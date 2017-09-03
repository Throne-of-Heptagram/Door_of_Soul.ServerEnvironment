using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Core;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    public class HexagramEntranceServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public static ServerPeer KnowledgePeer { get; private set; }
        public static ServerPeer LifePeer { get; private set; }
        public static ServerPeer ElementPeer { get; private set; }
        public static ServerPeer InfinitePeer { get; private set; }
        public static ServerPeer LovePeer { get; private set; }
        public static ServerPeer SpacePeer { get; private set; }
        public static ServerPeer WillPeer { get; private set; }
        public static ServerPeer ShadowPeer { get; private set; }
        public static ServerPeer HistoryPeer { get; private set; }
        public static ServerPeer EternityPeer { get; private set; }
        public static ServerPeer DestinyPeer { get; private set; }
        public static ServerPeer ThronePeer { get; private set; }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            EndPointType endPointType;
            switch (initRequest.LocalPort)
            {
                case int port when port == ServerEnvironmentConfiguration.Instance.ProxyServerListenPort:
                    endPointType = EndPointType.ProxyServer;
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.SceneServerListenPort:
                    endPointType = EndPointType.SceneServer;
                    break;
                default:
                    throw new System.NotSupportedException();
            }
            return new HexagramEntrancePeer(initRequest, endPointType);
        }

        protected override void Setup()
        {
            SetupLog();
            SetupConfiguration();
            SetupCommunicationService();
            //SetupServerConnection();
            Log.Info("HexagramEntranceServerApplication Setup.");
        }

        protected override void TearDown()
        {
            Log.Info("Server TearDown.");
            EndPointFactory.Instance.OnSubjectAdded -= LogEndPointConnected;
            EndPointFactory.Instance.OnSubjectRemoved -= LogEndPointDisconnected;
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

            EndPointFactory.Instance.OnSubjectAdded += LogEndPointConnected;
            EndPointFactory.Instance.OnSubjectRemoved += LogEndPointDisconnected;
        }

        private void SetupConfiguration()
        {
            ServerEnvironmentConfiguration serverEnvironmentConfiguration;
            if(GenericConfigurationLoader<ServerEnvironmentConfiguration>.Load(Path.Combine(ApplicationPath, "config", "ServerEnvironment.config"), out serverEnvironmentConfiguration))
            {
                ServerEnvironmentConfiguration.Initial(serverEnvironmentConfiguration);
            }
            else
            {
                throw new FileLoadException("ServerEnvironmentConfiguration Load Failed");
            }
        }

        private void SetupCommunicationService()
        {
            CommunicationService.Initial(new HexagramEntranceServerCommunicationService());
            KnowledgeCommunicationService.Initial(new HexagramEntranceServerKnowledgeCommunicationService());
            LifeCommunicationService.Initial(new HexagramEntranceServerLifeCommunicationService());
            ElementCommunicationService.Initial(new HexagramEntranceServeElementCommunicationService());
            InfiniteCommunicationService.Initial(new HexagramEntranceServerInfiniteCommunicationService());
            LoveCommunicationService.Initial(new HexagramEntranceServerLoveCommunicationService());
            SpaceCommunicationService.Initial(new HexagramEntranceServerSpaceCommunicationService());
            WillCommunicationService.Initial(new HexagramEntranceServeWillCommunicationService());
            ShadowCommunicationService.Initial(new HexagramEntranceServeShadowCommunicationService());
            HistoryCommunicationService.Initial(new HexagramEntranceServerHistoryCommunicationService());
            EternityCommunicationService.Initial(new HexagramEntranceServerEternityCommunicationService());
            DestinyCommunicationService.Initial(new HexagramEntranceServerDestinyCommunicationService());
            ThroneCommunicationService.Initial(new HexagramEntranceServerThroneCommunicationService());
        }

        private void SetupServerConnection()
        {
            KnowledgePeer = new ServerPeer(this);
            LifePeer = new ServerPeer(this);
            ElementPeer = new ServerPeer(this);
            InfinitePeer = new ServerPeer(this);
            LovePeer = new ServerPeer(this);
            SpacePeer = new ServerPeer(this);
            WillPeer = new ServerPeer(this);
            ShadowPeer = new ServerPeer(this);
            HistoryPeer = new ServerPeer(this);
            EternityPeer = new ServerPeer(this);
            DestinyPeer = new ServerPeer(this);
            ThronePeer = new ServerPeer(this);

            KnowledgeCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.KnowledgeServerAddress,
                port: ServerEnvironmentConfiguration.Instance.KnowledgeServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.KnowledgeServerApplicationName);
            LifeCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.LifeServerAddress,
                port: ServerEnvironmentConfiguration.Instance.LifeServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.LifeServerApplicationName);
            ElementCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ElementServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ElementServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ElementServerApplicationName);
            InfiniteCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.InfiniteServerAddress,
                port: ServerEnvironmentConfiguration.Instance.InfiniteServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.InfiniteServerApplicationName);
            LoveCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.LoveServerAddress,
                port: ServerEnvironmentConfiguration.Instance.LoveServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.LoveServerApplicationName);
            SpaceCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.SpaceServerAddress,
                port: ServerEnvironmentConfiguration.Instance.SpaceServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.SpaceServerApplicationName);
            WillCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.WillServerAddress,
                port: ServerEnvironmentConfiguration.Instance.WillServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.KnowledgeServerApplicationName);
            ShadowCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ShadowServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ShadowServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ShadowServerApplicationName);
            HistoryCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.HistoryServerAddress,
                port: ServerEnvironmentConfiguration.Instance.HistoryServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.HistoryServerApplicationName);
            EternityCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.EternityServerAddress,
                port: ServerEnvironmentConfiguration.Instance.EternityServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.EternityServerApplicationName);
            DestinyCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.DestinyServerAddress,
                port: ServerEnvironmentConfiguration.Instance.DestinyServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.DestinyServerApplicationName);
            ThroneCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ThroneServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ThroneServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ThroneServerApplicationName);
        }

        

        private void LogEndPointConnected(TerminalEndPoint endPoint)
        {
            Log.Info($"EndPoint: {endPoint} connected");
        }
        private void LogEndPointDisconnected(TerminalEndPoint endPoint)
        {
            Log.Info($"EndPoint: {endPoint} disconnected");
        }
    }
}
