using Door_of_Soul.Communication.HexagramCentralServer;
using Door_of_Soul.Core;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;

namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    class HexagramCentralServerEnvironment : ServerEnvironment.ServerEnvironment
    {
        public static HexagramNodePeer KnowledgePeer { get; set; }
        public static HexagramNodePeer LifePeer { get; set; }
        public static HexagramNodePeer ElementPeer { get; set; }
        public static HexagramNodePeer InfinitePeer { get; set; }
        public static HexagramNodePeer LovePeer { get; set; }
        public static HexagramNodePeer SpacePeer { get; set; }
        public static HexagramNodePeer WillPeer { get; set; }
        public static HexagramNodePeer ShadowPeer { get; set; }
        public static HexagramNodePeer HistoryPeer { get; set; }
        public static HexagramNodePeer EternityPeer { get; set; }
        public static HexagramNodePeer DestinyPeer { get; set; }
        public static HexagramNodePeer ThronePeer { get; set; }

        public override bool SetupCommunication(out string errorMessage)
        {
            CommunicationService.Initialize(new HexagramCentralServerCommunicationService());
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
            errorMessage = "";
            return true;
        }

        public override bool SetupServer(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public override void TearDown()
        {

        }

        public override bool SetupDatabase(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
