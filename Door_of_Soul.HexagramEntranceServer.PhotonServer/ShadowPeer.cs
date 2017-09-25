using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class ShadowPeer : OutboundS2SPeer
    {
        public ShadowPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramEntranceServerApplication.Log.Info($"ShadowServer ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramEntranceServerApplication.Log.Info($"ShadowServer ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramEntranceServerApplication.Log.Info($"ShadowServer Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramNodeServerReconnectDelayMillisecond);
                string errorMessage;
                HexagramEntranceServerEnvironment.ConnectHexagrameNodeServer(HexagramNodeServerType.Shadow, out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            ShadowEventCode eventCode = (ShadowEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!ShadowCommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"ShadowServer Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramEntranceServerApplication.Log.Error($"ShadowServer Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            ShadowOperationCode operationCode = (ShadowOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!ShadowCommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"ShadowServer OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
