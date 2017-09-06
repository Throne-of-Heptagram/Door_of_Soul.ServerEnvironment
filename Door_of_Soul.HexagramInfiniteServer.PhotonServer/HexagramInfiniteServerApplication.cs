using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramInfiniteServer.PhotonServer
{
    public class HexagramInfiniteServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new InfinitePeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramInfiniteServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramInfiniteServerApplication Setup.");
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
