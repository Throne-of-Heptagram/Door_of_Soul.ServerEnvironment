using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Space;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramSpaceServer.PhotonServer
{
    public class SpacePeer : InboundS2SPeer
    {
        public SpaceHexagramEntrance Entrance { get; private set; }

        public SpacePeer(InitRequest initRequest) : base(initRequest)
        {
            int hexagramEntranceId = (int)initRequest.InitObject;
            SpaceHexagramEntrance entrance;
            if (SpaceHexagramEntranceFactory.Instance.CreateEntrance(hexagramEntranceId, SendEvent, SendOperationResponse, out entrance))
            {
                Entrance = entrance;
            }
            else
            {
                throw new Exception("SpacePeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            SpaceHexagramEntranceFactory.Instance.Remove(Entrance.HexagramEntranceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            SpaceOperationCode operationCode = (SpaceOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!EntranceCommunicationService<SpaceEventCode, SpaceOperationCode, VirtualSpace>.Instance.HandleOperationRequest(Entrance, VirtualSpace.Instance, operationCode, parameters, out errorMessage))
            {
                HexagramSpaceServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(SpaceEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(SpaceOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
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
