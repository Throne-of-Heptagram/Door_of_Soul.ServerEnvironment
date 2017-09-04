﻿using ExitGames.Logging;
using Photon.SocketServer;

namespace Door_of_Soul.SceneServer.PhotonServer
{
    public class SceneServerApplication : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ScenePeer(initRequest);
        }

        protected override void Setup()
        {
            ServerEnvironment.ServerEnvironment.Initialize(new SceneServerEnvironment());
            string errorMessage;
            if(ServerEnvironment.ServerEnvironment.Instance.Setup(out errorMessage))
            {
                Log.Info("SceneServerApplication Setup.");
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
