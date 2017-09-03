using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.SceneServer;
using Door_of_Soul.Core.SceneServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.SceneServer.PhotonServer
{
    class SceneServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return SceneServerApplication.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            SceneServerApplication.ServerPeer.Disconnect();
        }

        public override bool FindWorld(int worldId, out VirtualWorld world)
        {
            ObservableWorld observableWorld;
            if (WorldFactory.Instance.Find(worldId, out observableWorld))
            {
                world = observableWorld;
                return true;
            }
            else
            {
                world = observableWorld;
                return false;
            }
        }

        public override bool FindScene(int sceneId, out TerminalScene scene)
        {
            ObservableScene observableScene;
            if (SceneFactory.Instance.Find(sceneId, out observableScene))
            {
                scene = observableScene;
                return true;
            }
            else
            {
                scene = observableScene;
                return false;
            }
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            SceneServerApplication.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
