using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.ObserverServer.PhotonServer
{
    public class ObserverServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ObserverPeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new ObserverServerEnvironment());
            string errorMessage;
            if(ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("ObserverServerApplication Setup.");
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
