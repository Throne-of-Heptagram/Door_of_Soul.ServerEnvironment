using Door_of_Soul.Communication.LoginServer;
using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.LoginServer.PhotonServer
{
    class LoginServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return LoginServerEnvironment.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, ServerEnvironmentConfiguration.Instance.EndPointId);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            LoginServerEnvironment.ServerPeer.Disconnect();
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            LoginServerEnvironment.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
