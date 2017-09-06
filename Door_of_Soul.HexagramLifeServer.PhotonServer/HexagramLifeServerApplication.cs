using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.HexagramLifeServer.PhotonServer
{
    public class HexagramLifeServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new LifePeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new HexagramLifeServerEnvironment());
            string errorMessage;
            if (ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("HexagramLifeServerApplication Setup.");
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
