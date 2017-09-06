using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramThroneServer.PhotonServer
{
    public class CentralPeer : OutboundS2SPeer
    {
        public CentralPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramThroneServerApplication.Log.Info($"Server ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramThroneServerApplication.Log.Info($"Server ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramThroneServerApplication.Log.Info($"Server Disconnect");
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            HexagramThroneServerApplication.Log.Error($"Server OnEvent");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            ThroneForwardOperationCode operationCode = (ThroneForwardOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!CentralCommunicationService.Instance.HandleForwardOperationRequest(operationCode, parameters, out errorMessage))
            {
                HexagramThroneServerApplication.Log.Info($"ForwardOperation Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            HexagramThroneServerApplication.Log.Error($"Server OnOperationResponse");
        }
    }
}
