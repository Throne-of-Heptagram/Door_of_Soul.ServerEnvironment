using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class WillPeer : OutboundS2SPeer
    {
        public WillPeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramEntranceServerApplication.Log.Info($"WillServer ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramEntranceServerApplication.Log.Info($"WillServer ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramEntranceServerApplication.Log.Info($"WillServer Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramNodeServerReconnectDelayMillisecond);
                string errorMessage;
                HexagramEntranceServerEnvironment.ConnectHexagrameNodeServer(HexagramNodeServerType.Will, out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            WillEventCode eventCode = (WillEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!WillCommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"WillServer Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramEntranceServerApplication.Log.Error($"WillServer Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            WillOperationCode operationCode = (WillOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!WillCommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"WillServer OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
