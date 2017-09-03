using Door_of_Soul.Communication.HexagramEntranceServer;
using Door_of_Soul.Core.HexagramEntranceServer;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    class HexagramEntranceServerCommunicationService : CommunicationService
    {
        public override bool FindAnswer(int answerId, out VirtualAnswer answer)
        {
            HexagramEntranceAnswer hexagramEntranceAnswer;
            if (AnswerFactory.Instance.Find(answerId, out hexagramEntranceAnswer))
            {
                answer = hexagramEntranceAnswer;
                return true;
            }
            else
            {
                answer = hexagramEntranceAnswer;
                return false;
            }
        }

        public override bool FindAvatar(int avatarId, out VirtualAvatar avatar)
        {
            HexagramEntranceAvatar hexagramEntranceAvatar;
            if (AvatarFactory.Instance.Find(avatarId, out hexagramEntranceAvatar))
            {
                avatar = hexagramEntranceAvatar;
                return true;
            }
            else
            {
                avatar = hexagramEntranceAvatar;
                return false;
            }
        }

        public override bool FindScene(int sceneId, out VirtualScene scene)
        {
            HexagramEntranceScene hexagramEntranceScene;
            if (SceneFactory.Instance.Find(sceneId, out hexagramEntranceScene))
            {
                scene = hexagramEntranceScene;
                return true;
            }
            else
            {
                scene = hexagramEntranceScene;
                return false;
            }
        }

        public override bool FindSoul(int soulId, out VirtualSoul soul)
        {
            HexagramEntranceSoul hexagramEntranceSoul;
            if (SoulFactory.Instance.Find(soulId, out hexagramEntranceSoul))
            {
                soul = hexagramEntranceSoul;
                return true;
            }
            else
            {
                soul = hexagramEntranceSoul;
                return false;
            }
        }

        public override bool FindWorld(int worldId, out VirtualWorld world)
        {
            HexagramEntranceWorld hexagramEntranceWorld;
            if (WorldFactory.Instance.Find(worldId, out hexagramEntranceWorld))
            {
                world = hexagramEntranceWorld;
                return true;
            }
            else
            {
                world = hexagramEntranceWorld;
                return false;
            }
        }
    }
}
