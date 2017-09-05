namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public int HexagramKnowledgeServerListenPort { get; set; } = 10016;
        public int HexagramLifeServerListenPort { get; set; } = 10017;
        public int HexagramElementServerListenPort { get; set; } = 10018;
        public int HexagramInfiniteServerListenPort { get; set; } = 10019;
        public int HexagramLoveServerListenPort { get; set; } = 10020;
        public int HexagramSpaceServerListenPort { get; set; } = 10021;
        public int HexagramWillServerListenPort { get; set; } = 10022;
        public int HexagramShadowServerListenPort { get; set; } = 10023;
        public int HexagramHistoryServerListenPort { get; set; } = 10024;
        public int HexagramEternityServerListenPort { get; set; } = 10025;
        public int HexagramDestinyServerListenPort { get; set; } = 10026;
        public int HexagramThroneServerListenPort { get; set; } = 10027;
    }
}
