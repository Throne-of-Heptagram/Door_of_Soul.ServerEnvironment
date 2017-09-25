using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class DestinyPeer : OutboundS2SPeer
    {
        public DestinyPeer(ApplicationBase application) : base(application)
        {

        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramEntranceServerApplication.Log.Info($"DestinyServer ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramEntranceServerApplication.Log.Info($"DestinyServer ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramEntranceServerApplication.Log.Info($"DestinyServer Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramNodeServerReconnectDelayMillisecond);
                string errorMessage;
                HexagramEntranceServerEnvironment.ConnectHexagrameNodeServer(HexagramNodeServerType.Destiny, out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            DestinyEventCode eventCode = (DestinyEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!DestinyCommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"DestinyServer Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramEntranceServerApplication.Log.Error($"DestinyServer Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            DestinyOperationCode operationCode = (DestinyOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!DestinyCommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"DestinyServer OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
