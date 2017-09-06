using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramShadowServer.PhotonServer
{
    public class ShadowPeer : InboundS2SPeer
    {
        public ShadowHexagramEntrance Entrance { get; private set; }

        public ShadowPeer(InitRequest initRequest) : base(initRequest)
        {
            ShadowHexagramEntrance entrance;
            if (ShadowHexagramEntranceFactory.Instance.CreateEntrance(SendEvent, SendOperationResponse, out entrance))
            {
                Entrance = entrance;
            }
            else
            {
                throw new Exception("ShadowPeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            ShadowHexagramEntranceFactory.Instance.Remove(Entrance.HexagramEntranceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            ShadowOperationCode operationCode = (ShadowOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!EntranceCommunicationService<ShadowEventCode, ShadowOperationCode>.Instance.HandleOperationRequest(Entrance, operationCode, parameters, out errorMessage))
            {
                HexagramShadowServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(ShadowEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(ShadowOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)returnCode,
                DebugMessage = operationMessage
            };
            SendOperationResponse(response, new SendParameters());
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
