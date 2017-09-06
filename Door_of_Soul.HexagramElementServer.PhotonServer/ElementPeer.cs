using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Element;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
namespace Door_of_Soul.HexagramElementServer.PhotonServer
{
    public class ElementPeer : InboundS2SPeer
    {
        public ElementHexagramEntrance Entrance { get; private set; }

        public ElementPeer(InitRequest initRequest) : base(initRequest)
        {
            ElementHexagramEntrance entrance;
            if (ElementHexagramEntranceFactory.Instance.CreateEntrance(SendEvent, SendOperationResponse, out entrance))
            {
                Entrance = entrance;
            }
            else
            {
                throw new Exception("ElementPeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            ElementHexagramEntranceFactory.Instance.Remove(Entrance.HexagramEntranceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            ElementOperationCode operationCode = (ElementOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!EntranceCommunicationService<ElementEventCode, ElementOperationCode>.Instance.HandleOperationRequest(Entrance, operationCode, parameters, out errorMessage))
            {
                HexagramElementServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(ElementEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(ElementOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
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
