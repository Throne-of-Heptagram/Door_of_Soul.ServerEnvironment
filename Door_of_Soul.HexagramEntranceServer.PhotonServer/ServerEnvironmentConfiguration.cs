namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initial(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public int ProxyServerListenPort { get; set; } = 10002;
        public int SceneServerListenPort { get; set; } = 10003;

        public string KnowledgeServerAddress { get; set; } = "127.0.0.1";
        public string LifeServerAddress { get; set; } = "127.0.0.1";
        public string ElementServerAddress { get; set; } = "127.0.0.1";
        public string InfiniteServerAddress { get; set; } = "127.0.0.1";
        public string LoveServerAddress { get; set; } = "127.0.0.1";
        public string SpaceServerAddress { get; set; } = "127.0.0.1";
        public string WillServerAddress { get; set; } = "127.0.0.1";
        public string ShadowServerAddress { get; set; } = "127.0.0.1";
        public string HistoryServerAddress { get; set; } = "127.0.0.1";
        public string EternityServerAddress { get; set; } = "127.0.0.1";
        public string DestinyServerAddress { get; set; } = "127.0.0.1";
        public string ThroneServerAddress { get; set; } = "127.0.0.1";

        public int KnowledgeServerPort { get; set; } = 10004;
        public int LifeServerPort { get; set; } = 10005;
        public int ElementServerPort { get; set; } = 10006;
        public int InfiniteServerPort { get; set; } = 10007;
        public int LoveServerPort { get; set; } = 10008;
        public int SpaceServerPort { get; set; } = 10009;
        public int WillServerPort { get; set; } = 100010;
        public int ShadowServerPort { get; set; } = 10011;
        public int HistoryServerPort { get; set; } = 10012;
        public int EternityServerPort { get; set; } = 10013;
        public int DestinyServerPort { get; set; } = 100014;
        public int ThroneServerPort { get; set; } = 100015;

        public string KnowledgeServerApplicationName { get; set; } = "Knowledge";
        public string LifeServerApplicationName { get; set; } = "Life";
        public string ElementServerApplicationName { get; set; } = "Element";
        public string InfiniteServerApplicationName { get; set; } = "Infinite";
        public string LoveServerApplicationName { get; set; } = "Love";
        public string SpaceServerApplicationName { get; set; } = "Space";
        public string WillServerApplicationName { get; set; } = "Will";
        public string ShadowServerApplicationName { get; set; } = "Shadow";
        public string HistoryServerApplicationName { get; set; } = "History";
        public string EternityServerApplicationName { get; set; } = "Eternity";
        public string DestinyServerApplicationName { get; set; } = "Destiny";
        public string ThroneServerApplicationName { get; set; } = "Throne";
    }
}
