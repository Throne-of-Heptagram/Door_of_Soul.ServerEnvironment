using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;
using Door_of_Soul.Communication.Protocol.Hexagram.Knowledge;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerKnowledgeCommunicationService : KnowledgeCommunicationService
    {
        public override bool ConnectServer(string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.KnowledgePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.KnowledgePeer.Disconnect();
        }

        public override void SendOperation(KnowledgeOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.KnowledgePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
