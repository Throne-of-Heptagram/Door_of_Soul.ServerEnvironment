using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.ProxyServer.PhotonServer
{
    public class ProxyServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ProxyPeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new ProxyServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("ProxyServerApplication Setup.");
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
