﻿using Door_of_Soul.Communication.ObserverServer;
using Door_of_Soul.Communication.Protocol.External.Device;
using Door_of_Soul.Core.Protocol;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Door_of_Soul.ObserverServer.PhotonServer
{
    public class ObserverPeer : ClientPeer
    {
        public TerminalDevice Device { get; private set; }

        public ObserverPeer(InitRequest initRequest) : base(initRequest)
        {
            TerminalDevice device;
            if (DeviceFactory.Instance.CreateDevice(SendEvent, SendOperationResponse, out device))
            {
                Device = device;
            }
            else
            {
                throw new Exception("ObserverPeer CreateDevice Fail");
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            DeviceFactory.Instance.Remove(Device.DeviceId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            DeviceOperationCode operationCode = (DeviceOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!CommunicationService.Instance.HandleOperationRequest(Device, operationCode, parameters, out errorMessage))
            {
                ObserverServerApplication.Log.Info($"OperationRequest Fail, ErrorMessage: {errorMessage}");
            }
        }

        private void SendEvent(DeviceEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            SendEvent(eventData, new SendParameters());
        }
        private void SendOperationResponse(DeviceOperationCode operationCode, OperationReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)returnCode,
                DebugMessage = operationMessage
            };
            SendOperationResponse(response, new SendParameters());
        }
    }
}
