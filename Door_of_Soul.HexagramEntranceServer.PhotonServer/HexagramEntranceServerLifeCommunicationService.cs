using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;
using Door_of_Soul.Communication.Protocol.Hexagram.Life;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerLifeCommunicationService : LifeCommunicationService
    {
        public override bool ConnectServer(int hexagramEntranceId, string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.LifePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, hexagramEntranceId);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.LifePeer.Disconnect();
        }

        public override void SendOperation(LifeOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.LifePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
