using Door_of_Soul.Communication.LoginServer;
using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

namespace Door_of_Soul.LoginServer.PhotonServer
{
    public class ServerPeer : OutboundS2SPeer
    {
        public ServerPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            LoginServerApplication.Log.Info($"Server ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            LoginServerApplication.Log.Info($"Server ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            LoginServerApplication.Log.Info($"Server Disconnect");
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            EndPointEventCode eventCode = (EndPointEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!CommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                LoginServerApplication.Log.Info($"Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            LoginServerApplication.Log.Error($"Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            EndPointOperationCode operationCode = (EndPointOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!CommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                LoginServerApplication.Log.Info($"OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
