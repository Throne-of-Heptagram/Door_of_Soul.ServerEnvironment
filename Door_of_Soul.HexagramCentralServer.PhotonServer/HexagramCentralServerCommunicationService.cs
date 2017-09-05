using Door_of_Soul.Communication.HexagramCentralServer;
using Door_of_Soul.Communication.Protocol.Hexagram.Destiny;
using Door_of_Soul.Communication.Protocol.Hexagram.Element;
using Door_of_Soul.Communication.Protocol.Hexagram.Eternity;
using Door_of_Soul.Communication.Protocol.Hexagram.History;
using Door_of_Soul.Communication.Protocol.Hexagram.Infinite;
using Door_of_Soul.Communication.Protocol.Hexagram.Knowledge;
using Door_of_Soul.Communication.Protocol.Hexagram.Life;
using Door_of_Soul.Communication.Protocol.Hexagram.Love;
using Door_of_Soul.Communication.Protocol.Hexagram.Shadow;
using Door_of_Soul.Communication.Protocol.Hexagram.Space;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne;
using Door_of_Soul.Communication.Protocol.Hexagram.Will;
using Photon.SocketServer;
using System.Collections.Generic;

namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    class HexagramCentralServerCommunicationService : CommunicationService
    {
        public override void SendForwardOperation(KnowledgeForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.KnowledgePeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(LifeForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.LifePeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(ElementForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.ElementPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(InfiniteForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.InfinitePeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(LoveForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.LovePeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(SpaceForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.SpacePeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(WillForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.WillPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(ShadowForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.ShadowPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(HistoryForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.HistoryPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(EternityForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.EternityPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(DestinyForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.DestinyPeer.SendOperationRequest(request, new SendParameters());
        }

        public override void SendForwardOperation(ThroneForwardOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            HexagramCentralServerEnvironment.ThronePeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
