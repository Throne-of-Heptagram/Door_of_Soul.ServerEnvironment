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
        public override bool ConnectServer(string serverAddress, int port, string applicationName)
        {
            return HexagramEntranceServerApplication.LifePeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectServer()
        {
            HexagramEntranceServerApplication.LifePeer.Disconnect();
        }

        public override void SendOperation(LifeOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramEntranceServerApplication.LifePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
