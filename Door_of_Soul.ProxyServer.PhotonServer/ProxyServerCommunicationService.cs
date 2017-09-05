using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Core.ProxyServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.ProxyServer.PhotonServer
{
    class ProxyServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return ProxyServerApplication.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            ProxyServerApplication.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            ProxyServerApplication.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
