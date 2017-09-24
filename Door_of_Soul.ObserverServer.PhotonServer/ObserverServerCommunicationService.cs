using Door_of_Soul.Communication.ObserverServer;
using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.ObserverServer.PhotonServer
{
    class ObserverServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return ObserverServerEnvironment.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            ObserverServerEnvironment.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            ObserverServerEnvironment.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
