using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServeWillCommunicationService : WillCommunicationService
    {
        public override bool ConnectServer(int hexagramEntranceId, string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.WillPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName, hexagramEntranceId);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.WillPeer.Disconnect();
        }

        public override void SendOperation(WillOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.WillPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
