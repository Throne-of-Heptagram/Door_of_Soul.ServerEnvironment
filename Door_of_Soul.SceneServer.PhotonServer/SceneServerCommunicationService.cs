using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.SceneServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.SceneServer.PhotonServer
{
    class SceneServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return SceneServerEnvironment.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            SceneServerEnvironment.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            SceneServerEnvironment.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
