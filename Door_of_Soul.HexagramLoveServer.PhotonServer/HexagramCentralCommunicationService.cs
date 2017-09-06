using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Hexagram;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.HexagramLoveServer.PhotonServer
{
    class HexagramCentralCommunicationService : CentralCommunicationService
    {
        public override bool ConnectHexagrameCentralServer(string serverAddress, int port, string applicationName)
        {
            return HexagramLoveServerEnvironment.CentralPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameCentralServer()
        {
            HexagramLoveServerEnvironment.CentralPeer.Disconnect();
        }

        public override void SendForwardOperation(HexagramForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramLoveServerEnvironment.CentralPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
