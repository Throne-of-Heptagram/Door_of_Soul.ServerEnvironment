using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.ProxyServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.ProxyServer.PhotonServer
{
    class ProxyServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return ProxyServerEnvironment.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            ProxyServerEnvironment.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            ProxyServerEnvironment.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
