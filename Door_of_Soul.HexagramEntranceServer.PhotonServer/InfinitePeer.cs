using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Infinite;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class InfinitePeer : OutboundS2SPeer
    {
        public InfinitePeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramEntranceServerApplication.Log.Info($"InfiniteServer ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramEntranceServerApplication.Log.Info($"InfiniteServer ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramEntranceServerApplication.Log.Info($"InfiniteServer Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramNodeServerReconnectDelayMillisecond);
                string errorMessage;
                HexagramEntranceServerEnvironment.ConnectHexagrameNodeServer(HexagramNodeServerType.Infinite, out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            InfiniteEventCode eventCode = (InfiniteEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!InfiniteCommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"InfiniteServer Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramEntranceServerApplication.Log.Error($"InfiniteServer Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            InfiniteOperationCode operationCode = (InfiniteOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!InfiniteCommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"InfiniteServer OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
