using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Infinite;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerInfiniteCommunicationService : InfiniteCommunicationService
    {
        public override bool ConnectServer(string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerEnvironment.InfinitePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerEnvironment.InfinitePeer.Disconnect();
        }

        public override void SendOperation(InfiniteOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerEnvironment.InfinitePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
