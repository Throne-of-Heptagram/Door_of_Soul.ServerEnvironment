using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerThroneCommunicationService : ThroneCommunicationService
    {
        public override bool ConnectServer(string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.ThronePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.ThronePeer.Disconnect();
        }

        public override void SendOperation(ThroneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.ThronePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
