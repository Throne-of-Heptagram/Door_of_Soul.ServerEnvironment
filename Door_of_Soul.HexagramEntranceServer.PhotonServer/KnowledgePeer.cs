using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Knowledge;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class KnowledgePeer : OutboundS2SPeer
    {
        public KnowledgePeer(ApplicationBase application) : base(application)
        {
        }

        protected override void OnConnectionEstablished(object responseObject)
        {
            HexagramEntranceServerApplication.Log.Info($"KnowledgeServer ConnectionEstablished");
        }

        protected override void OnConnectionFailed(int errorCode, string errorMessage)
        {
            HexagramEntranceServerApplication.Log.Info($"KnowledgeServer ConnectionFailed");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            HexagramEntranceServerApplication.Log.Info($"KnowledgeServer Disconnect");
            Task.Run(async () =>
            {
                await Task.Delay(ServerEnvironmentConfiguration.Instance.HexagramNodeServerReconnectDelayMillisecond);
                string errorMessage;
                HexagramEntranceServerEnvironment.ConnectHexagrameNodeServer(HexagramNodeServerType.Knowledge, out errorMessage);
            });
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            KnowledgeEventCode eventCode = (KnowledgeEventCode)eventData.Code;
            Dictionary<byte, object> parameters = eventData.Parameters;

            string errorMessage;
            if (!KnowledgeCommunicationService.Instance.HandleEvent(eventCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"KnowledgeServer Event Fail, ErrorMessage: {errorMessage}");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HexagramEntranceServerApplication.Log.Error($"KnowledgeServer Server OperationRequest");
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            KnowledgeOperationCode operationCode = (KnowledgeOperationCode)operationResponse.OperationCode;
            OperationReturnCode returnCode = (OperationReturnCode)operationResponse.ReturnCode;
            string operationMessage = operationResponse.DebugMessage;
            Dictionary<byte, object> parameters = operationResponse.Parameters;

            string errorMessage;
            if (!KnowledgeCommunicationService.Instance.HandleOperationResponse(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"KnowledgeServer OperationResponse Fail, ErrorMessage: {errorMessage}");
            }
        }
    }
}
