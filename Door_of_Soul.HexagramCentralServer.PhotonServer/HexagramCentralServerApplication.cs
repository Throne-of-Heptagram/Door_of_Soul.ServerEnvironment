using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    public class HexagramCentralServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            HexagramNodePeer peer = new HexagramNodePeer(initRequest);
            switch (initRequest.LocalPort)
            {
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramKnowledgeServerListenPort:
                    HexagramCentralServerEnvironment.KnowledgePeer = peer;
                    Log.Info("HexagramCentralServer:Knowledge ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramLifeServerListenPort:
                    HexagramCentralServerEnvironment.LifePeer = peer;
                    Log.Info("HexagramCentralServer:Life ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramElementServerListenPort:
                    HexagramCentralServerEnvironment.ElementPeer = peer;
                    Log.Info("HexagramCentralServer:Element ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramInfiniteServerListenPort:
                    HexagramCentralServerEnvironment.InfinitePeer = peer;
                    Log.Info("HexagramCentralServer:Infinite ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramLoveServerListenPort:
                    HexagramCentralServerEnvironment.LovePeer = peer;
                    Log.Info("HexagramCentralServer:Love ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramSpaceServerListenPort:
                    HexagramCentralServerEnvironment.SpacePeer = peer;
                    Log.Info("HexagramCentralServer:Space ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramWillServerListenPort:
                    HexagramCentralServerEnvironment.WillPeer = peer;
                    Log.Info("HexagramCentralServer:Will ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramShadowServerListenPort:
                    HexagramCentralServerEnvironment.ShadowPeer = peer;
                    Log.Info("HexagramCentralServer:Shadow ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramHistoryServerListenPort:
                    HexagramCentralServerEnvironment.HistoryPeer = peer;
                    Log.Info("HexagramCentralServer:History ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramEternityServerListenPort:
                    HexagramCentralServerEnvironment.EternityPeer = peer;
                    Log.Info("HexagramCentralServer:Eternity ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramDestinyServerListenPort:
                    HexagramCentralServerEnvironment.DestinyPeer = peer;
                    Log.Info("HexagramCentralServer:Destiny ready");
                    break;
                case int port when port == ServerEnvironmentConfiguration.Instance.HexagramThroneServerListenPort:
                    HexagramCentralServerEnvironment.ThronePeer = peer;
                    Log.Info("HexagramCentralServer:Throne ready");
                    break;
                default:
                    Log.Fatal($"HexagramCentralServerApplication Accept from Port{initRequest.LocalPort}.");
                    break;
            }
            return peer;
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramCentralServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramCentralServerApplication Setup.");
            }
            else
            {
                Log.Fatal(errorMessage);
                TearDown();
            }
        }

        protected override void TearDown()
        {
            ServerEnvironment.ServerEnvironment.Instance.TearDown();
            Log.Info("Server TearDown.");
        }
    }
}
