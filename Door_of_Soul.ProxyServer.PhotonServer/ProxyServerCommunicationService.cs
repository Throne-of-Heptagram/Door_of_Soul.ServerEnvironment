using Door_of_Soul.Communication.Protocol.Internal.EndPoint;
using Door_of_Soul.Communication.ProxyServer;
using Door_of_Soul.Core.ProxyServer;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Net;

namespace Door_of_Soul.ProxyServer.PhotonServer
{
    class ProxyServerCommunicationService : CommunicationService
    {
        public override bool ConnectHexagrameEntranceServer(string serverAddress, int port, string applicationName)
        {
            return ProxyServerApplication.ServerPeer.ConnectTcp(new IPEndPoint(IPAddress.Parse(serverAddress), port), applicationName);
        }

        public override void DisconnectHexagrameEntranceServer()
        {
            ProxyServerApplication.ServerPeer.Disconnect();
        }

        public override bool FindAnswer(int answerId, out TerminalAnswer answer)
        {
            ProxyAnswer proxyAnswer;
            if(AnswerFactory.Instance.Find(answerId, out proxyAnswer))
            {
                answer = proxyAnswer;
                return true;
            }
            else
            {
                answer = proxyAnswer;
                return false;
            }
        }

        public override bool FindAvatar(int avatarId, out VirtualAvatar avatar)
        {
            ProxyAvatar proxyAvatar;
            if (AvatarFactory.Instance.Find(avatarId, out proxyAvatar))
            {
                avatar = proxyAvatar;
                return true;
            }
            else
            {
                avatar = proxyAvatar;
                return false;
            }
        }

        public override bool FindSoul(int soulId, out VirtualSoul soul)
        {
            ProxySoul proxySoul;
            if (SoulFactory.Instance.Find(soulId, out proxySoul))
            {
                soul = proxySoul;
                return true;
            }
            else
            {
                soul = proxySoul;
                return false;
            }
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            OperationRequest request = new OperationRequest
            {
                OperationCode = (byte)operationCode,
                Parameters = parameters
            };
            ProxyServerApplication.ServerPeer.SendOperationRequest(request, new SendParameters());
        }
    }
}
