using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramDestinyServer.PhotonServer
{
    public class DestinyPeer : InboundS2SPeer
    {
        public DestinyHexagramEntrance Entrance { get; private set; }

        public DestinyPeer(InitRequest initRequest) : base(initRequest)
        {
            int hexagramEntranceId = (int)initRequest.InitObject;
            DestinyHexagramEntrance entrance;
            if (DestinyHexagramEntranceFactory.Instance.CreateEntrance(hexagramEntranceId, SendEvent, SendOperationResponse, out entrance))
            {
                Entrance = entrance;
            }
            else
            {
                throw new Exception("DestinyPeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            DestinyHexagramEntranceFactory.Instance.Remove(Entrance.HexagramEntranceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            DestinyOperationCode operationCode = (DestinyOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!EntranceCommunicationService<DestinyEventCode, DestinyOperationCode, VirtualDestiny>.Instance.HandleOperationRequest(Entrance, VirtualDestiny.Instance, operationCode, parameters, out errorMessage))
            {
                HexagramDestinyServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(DestinyEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(DestinyOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
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
