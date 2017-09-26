namespace Door_of_Soul.LoginServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public int EndPointId { get; set; } = 1;

        public string HexagramEntranceServerAddress { get; set; } = "127.0.0.1";
        public int HexagramEntranceServerPort { get; set; } = 10025;
        public string HexagramEntranceServerApplicationName { get; set; } = "HexagramEntrance";

        public int HexagramEntranceServerReconnectDelayMillisecond { get; set; } = 10000;

        public string DatabaseServerAddress { get; set; } = "127.0.0.1";
        public int DatabasePort { get; set; } = 10000;
        public string DatabaseUsername { get; set; } = "";
        public string DatabasePassword { get; set; } = "";
        public string DatabasePrefix { get; set; } = "DS.Dev";
        public string DatabaseCharset { get; set; } = "utf8mb4";
    }
}
