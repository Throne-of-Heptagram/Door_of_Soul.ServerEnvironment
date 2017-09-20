using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Core;
using Door_of_Soul.Server;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;
using System.Threading;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerEnvironment : ServerEnvironment.ServerEnvironment
    {
        public static KnowledgePeer KnowledgePeer { get; private set; }
        public static LifePeer LifePeer { get; private set; }
        public static ElementPeer ElementPeer { get; private set; }
        public static InfinitePeer InfinitePeer { get; private set; }
        public static LovePeer LovePeer { get; private set; }
        public static SpacePeer SpacePeer { get; private set; }
        public static WillPeer WillPeer { get; private set; }
        public static ShadowPeer ShadowPeer { get; private set; }
        public static HistoryPeer HistoryPeer { get; private set; }
        public static EternityPeer EternityPeer { get; private set; }
        public static DestinyPeer DestinyPeer { get; private set; }
        public static ThronePeer ThronePeer { get; private set; }

        public override bool SetupCommunication(out string errorMessage)
        {
            CommunicationService.Initialize(new HexagramEntranceServerCommunicationService());
            KnowledgeCommunicationService.Initialize(new HexagramEntranceServerKnowledgeCommunicationService());
            LifeCommunicationService.Initialize(new HexagramEntranceServerLifeCommunicationService());
            ElementCommunicationService.Initialize(new HexagramEntranceServeElementCommunicationService());
            InfiniteCommunicationService.Initialize(new HexagramEntranceServerInfiniteCommunicationService());
            LoveCommunicationService.Initialize(new HexagramEntranceServerLoveCommunicationService());
            SpaceCommunicationService.Initialize(new HexagramEntranceServerSpaceCommunicationService());
            WillCommunicationService.Initialize(new HexagramEntranceServeWillCommunicationService());
            ShadowCommunicationService.Initialize(new HexagramEntranceServeShadowCommunicationService());
            HistoryCommunicationService.Initialize(new HexagramEntranceServerHistoryCommunicationService());
            EternityCommunicationService.Initialize(new HexagramEntranceServerEternityCommunicationService());
            DestinyCommunicationService.Initialize(new HexagramEntranceServerDestinyCommunicationService());
            ThroneCommunicationService.Initialize(new HexagramEntranceServerThroneCommunicationService());

            KnowledgePeer = new KnowledgePeer(ApplicationBase.Instance);
            LifePeer = new LifePeer(ApplicationBase.Instance);
            ElementPeer = new ElementPeer(ApplicationBase.Instance);
            InfinitePeer = new InfinitePeer(ApplicationBase.Instance);
            LovePeer = new LovePeer(ApplicationBase.Instance);
            SpacePeer = new SpacePeer(ApplicationBase.Instance);
            WillPeer = new WillPeer(ApplicationBase.Instance);
            ShadowPeer = new ShadowPeer(ApplicationBase.Instance);
            HistoryPeer = new HistoryPeer(ApplicationBase.Instance);
            EternityPeer = new EternityPeer(ApplicationBase.Instance);
            DestinyPeer = new DestinyPeer(ApplicationBase.Instance);
            ThronePeer = new ThronePeer(ApplicationBase.Instance);

            Thread.Sleep(ServerEnvironmentConfiguration.Instance.SetupConnectionDelay);

            if (!KnowledgeCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.KnowledgeServerAddress,
                port: ServerEnvironmentConfiguration.Instance.KnowledgeServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.KnowledgeServerApplicationName))
            {
                errorMessage = "ConnectHexagrameKnowledgeServer Failed";
                return false;
            }
            if (!LifeCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.LifeServerAddress,
                port: ServerEnvironmentConfiguration.Instance.LifeServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.LifeServerApplicationName))
            {
                errorMessage = "ConnectHexagrameLifeServer Failed";
                return false;
            }
            if (!ElementCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ElementServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ElementServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ElementServerApplicationName))
            {
                errorMessage = "ConnectHexagrameElementServer Failed";
                return false;
            }
            if (!InfiniteCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.InfiniteServerAddress,
                port: ServerEnvironmentConfiguration.Instance.InfiniteServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.InfiniteServerApplicationName))
            {
                errorMessage = "ConnectHexagrameInfiniteServer Failed";
                return false;
            }
            if (!LoveCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.LoveServerAddress,
                port: ServerEnvironmentConfiguration.Instance.LoveServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.LoveServerApplicationName))
            {
                errorMessage = "ConnectHexagrameLoveServer Failed";
                return false;
            }
            if (!SpaceCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.SpaceServerAddress,
                port: ServerEnvironmentConfiguration.Instance.SpaceServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.SpaceServerApplicationName))
            {
                errorMessage = "ConnectHexagrameSpaceServer Failed";
                return false;
            }
            if (!WillCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.WillServerAddress,
                port: ServerEnvironmentConfiguration.Instance.WillServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.KnowledgeServerApplicationName))
            {
                errorMessage = "ConnectHexagrameKnowledgeServer Failed";
                return false;
            }
            if (!ShadowCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ShadowServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ShadowServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ShadowServerApplicationName))
            {
                errorMessage = "ConnectHexagrameShadowServer Failed";
                return false;
            }
            if (!HistoryCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.HistoryServerAddress,
                port: ServerEnvironmentConfiguration.Instance.HistoryServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.HistoryServerApplicationName))
            {
                errorMessage = "ConnectHexagrameHistoryServer Failed";
                return false;
            }
            if (!EternityCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.EternityServerAddress,
                port: ServerEnvironmentConfiguration.Instance.EternityServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.EternityServerApplicationName))
            {
                errorMessage = "ConnectHexagrameEternityServer Failed";
                return false;
            }
            if (!DestinyCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.DestinyServerAddress,
                port: ServerEnvironmentConfiguration.Instance.DestinyServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.DestinyServerApplicationName))
            {
                errorMessage = "ConnectHexagrameDestinyServer Failed";
                return false;
            }
            if (!ThroneCommunicationService.Instance.ConnectServer(
                serverAddress: ServerEnvironmentConfiguration.Instance.ThroneServerAddress,
                port: ServerEnvironmentConfiguration.Instance.ThroneServerPort,
                applicationName: ServerEnvironmentConfiguration.Instance.ThroneServerApplicationName))
            {
                errorMessage = "ConnectHexagrameThroneServer Failed";
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

            EndPointFactory.Instance.OnSubjectAdded += LogEndPointConnected;
            EndPointFactory.Instance.OnSubjectRemoved += LogEndPointDisconnected;

            errorMessage = "";
            return true;
        }

        public override bool SetupServer(out string errorMessage)
        {
            ServerInitializer.Initialize(new HexagramEntranceServerInitializer());
            return ServerInitializer.Instance.Initialize(out errorMessage);
        }

        public override void TearDown()
        {
            EndPointFactory.Instance.OnSubjectAdded -= LogEndPointConnected;
            EndPointFactory.Instance.OnSubjectRemoved -= LogEndPointDisconnected;
        }

        private void LogEndPointDisconnected(TerminalEndPoint endPoint)
        {
            HexagramEntranceServerApplication.Log.Info($"EndPoint: {endPoint} disconnected");
        }

        private void LogEndPointConnected(TerminalEndPoint endPoint)
        {
            HexagramEntranceServerApplication.Log.Info($"EndPoint: {endPoint} connected");
        }

        public override bool SetupDatabase(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
