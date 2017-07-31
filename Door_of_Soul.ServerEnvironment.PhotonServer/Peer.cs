using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;

namespace Door_of_Soul.ServerEnvironment.PhotonServer
{
    public class Peer : ClientPeer
    {
        public Peer(InitRequest initRequest) : base(initRequest)
        {
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }
    }
}
