using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.History;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerHistoryCommunicationService : HistoryCommunicationService
    {
        public override bool ConnectServer(string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.HistoryPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.HistoryPeer.Disconnect();
        }

        public override void SendOperation(HistoryOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.HistoryPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
