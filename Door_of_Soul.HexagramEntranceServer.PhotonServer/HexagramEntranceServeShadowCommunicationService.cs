using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServeShadowCommunicationService : ShadowCommunicationService
    {
        public override bool ConnectServer(int hexagramEntranceId, string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.ShadowPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, hexagramEntranceId);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.ShadowPeer.Disconnect();
        }

        public override void SendOperation(ShadowOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.ShadowPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
