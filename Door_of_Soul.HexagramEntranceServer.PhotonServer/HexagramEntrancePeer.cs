using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    public class HexagramEntrancePeer : InboundS2SPeer
    {
        public TerminalEndPoint EndPoint { get; private set; }

        public HexagramEntrancePeer(InitRequest initRequest, EndPointType endPointType) : base(initRequest)
        {
            TerminalEndPoint endPoint;
            
            if (EndPointFactory.Instance.CreateEndPoint(endPointType, SendEvent, SendOperationResponse, out endPoint))
            {
                EndPoint = endPoint;
            }
            else
            {
                throw new Exception("HexagramEntrancePeer CreateEndPoint Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            EndPointFactory.Instance.Remove(EndPoint.EndPointId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            EndPointOperationCode operationCode = (EndPointOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!CommunicationService.Instance.HandleOperationRequest(EndPoint, operationCode, parameters, out errorMessage))
            {
                HexagramEntranceServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(EndPointOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
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
