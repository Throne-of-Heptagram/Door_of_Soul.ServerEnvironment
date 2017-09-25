using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Eternity;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerEternityCommunicationService : EternityCommunicationService
    {
        public override bool ConnectServer(int hexagramEntranceId, string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.EternityPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, hexagramEntranceId);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.EternityPeer.Disconnect();
        }

        public override void SendOperation(EternityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.EternityPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
