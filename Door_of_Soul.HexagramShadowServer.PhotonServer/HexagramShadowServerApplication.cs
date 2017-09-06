using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramShadowServer.PhotonServer
{
    public class HexagramShadowServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ShadowPeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramShadowServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramShadowServerApplication Setup.");
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
