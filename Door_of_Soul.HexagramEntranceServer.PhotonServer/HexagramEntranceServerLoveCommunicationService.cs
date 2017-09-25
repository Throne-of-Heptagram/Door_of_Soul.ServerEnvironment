using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Love;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerLoveCommunicationService : LoveCommunicationService
    {
        public override bool ConnectServer(int hexagramEntranceId, string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.LovePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, hexagramEntranceId);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.LovePeer.Disconnect();
        }

        public override void SendOperation(LoveOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.LovePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
