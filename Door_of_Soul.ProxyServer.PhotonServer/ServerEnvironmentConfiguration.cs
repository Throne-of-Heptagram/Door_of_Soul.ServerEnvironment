namespace Door_of_Soul.ProxyServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public string HexagramEntranceServerAddress { get; set; } = "127.0.0.1";

        public int HexagramEntranceServerPort { get; set; } = 10025;

        public string HexagramEntranceServerApplicationName { get; set; } = "HexagramEntrance";

        public int SetupConnectionDelay { get; set; } = 10000;
    }
}
