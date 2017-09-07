namespace Door_of_Soul.HexagramCentralServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public int HexagramKnowledgeServerListenPort { get; set; } = 10001;
        public int HexagramLifeServerListenPort { get; set; } = 10002;
        public int HexagramElementServerListenPort { get; set; } = 10003;
        public int HexagramInfiniteServerListenPort { get; set; } = 10004;
        public int HexagramLoveServerListenPort { get; set; } = 10005;
        public int HexagramSpaceServerListenPort { get; set; } = 10006;
        public int HexagramWillServerListenPort { get; set; } = 10007;
        public int HexagramShadowServerListenPort { get; set; } = 10008;
        public int HexagramHistoryServerListenPort { get; set; } = 10009;
        public int HexagramEternityServerListenPort { get; set; } = 10010;
        public int HexagramDestinyServerListenPort { get; set; } = 10011;
        public int HexagramThroneServerListenPort { get; set; } = 10012;
    }
}
