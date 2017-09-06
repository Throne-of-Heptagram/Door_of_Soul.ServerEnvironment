using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramShadowServer.PhotonServer
{
    public class CentralPeer : OutboundS2SPeer
    {
        public CentralPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramShadowServerApplication.Log.Info($"Server ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramShadowServerApplication.Log.Info($"Server ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramShadowServerApplication.Log.Info($"Server Disconnect");
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            HexagramShadowServerApplication.Log.Error($"Server OnEvent");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            ShadowForwardOperationCode operationCode = (ShadowForwardOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!CentralCommunicationService.Instance.HandleForwardOperationRequest(operationCode, parameters, out errorMessage))
            {
                HexagramShadowServerApplication.Log.Info($"ForwardOperation Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            HexagramShadowServerApplication.Log.Error($"Server OnOperationResponse");
        }
    }
}
