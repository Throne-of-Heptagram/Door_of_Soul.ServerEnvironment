using Door_of_Soul.Communication.HexagramEntranceServer;
using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    public class HexagramEntranceServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

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
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramEntranceServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramEntranceServerApplication Setup.");
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
