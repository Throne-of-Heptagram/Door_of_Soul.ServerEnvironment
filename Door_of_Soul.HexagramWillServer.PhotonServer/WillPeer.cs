using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramWillServer.PhotonServer
{
    public class WillPeer : InboundS2SPeer
    {
        public WillHexagramEntrance Entrance { get; private set; }

        public WillPeer(InitRequest initRequest) : base(initRequest)
        {
            int hexagramEntranceId = (int)initRequest.InitObject;
            WillHexagramEntrance entrance;
            if (WillHexagramEntranceFactory.Instance.CreateEntrance(hexagramEntranceId, SendEvent, SendOperationResponse, out entrance))
            {
                Entrance = entrance;
            }
            else
            {
                throw new Exception("WillPeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            WillHexagramEntranceFactory.Instance.Remove(Entrance.HexagramEntranceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            WillOperationCode operationCode = (WillOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!EntranceCommunicationService<WillEventCode, WillOperationCode, VirtualWill>.Instance.HandleOperationRequest(Entrance, VirtualWill.Instance, operationCode, parameters, out errorMessage))
            {
                HexagramWillServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(WillEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(WillOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
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
