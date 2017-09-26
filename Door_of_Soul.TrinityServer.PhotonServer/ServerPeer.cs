using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.TrinityServer;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.TrinityServer.PhotonServer
{
    public class ServerPeer : OutboundS2SPeer
    {
        public ServerPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            TrinityServerApplication.Log.Info($"Server ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            TrinityServerApplication.Log.Info($"Server ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            TrinityServerApplication.Log.Info($"Server Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramEntranceServerReconnectDelayMillisecond);
                string errorMessage;
                TrinityServerEnvironment.ConnectHexagrameEntranceServer(out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            EndPointEventCode eventCode = (EndPointEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!CommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                TrinityServerApplication.Log.Info($"Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            TrinityServerApplication.Log.Error($"Server OperationRequest");
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
                TrinityServerApplication.Log.Info($"OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
