using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramSpaceServer.PhotonServer
{
    public class HexagramSpaceServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new SpacePeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramSpaceServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramSpaceServerApplication Setup.");
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
