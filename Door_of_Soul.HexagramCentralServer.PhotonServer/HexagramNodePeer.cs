using Door_of_Soul.Communication.HexagramCentralServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Hexagram;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    public class HexagramNodePeer : InboundS2SPeer
    {
        public HexagramNodePeer(InitRequest initRequest) : base(initRequest)
        {

        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramForwardOperationCode operationCode = (HexagramForwardOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;
            string errorMessage;
            if(!CommunicationService.Instance.HandleForwardOperationRequest(operationCode, parameters, out errorMessage))
            {
                HexagramCentralServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }
    }
}
