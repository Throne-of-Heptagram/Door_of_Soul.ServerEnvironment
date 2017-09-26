using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.TrinityServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.TrinityServer.PhotonServer
{
    class TrinityServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return TrinityServerEnvironment.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, ServerEnvironmentConfiguration.Instance.EndPointId);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            TrinityServerEnvironment.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            TrinityServerEnvironment.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
