namespace Door_of_Soul.HexagramThroneServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public string HexagramCentralServerAddress { get; set; } = "127.0.0.1";
        public int HexagramCentralServerPort { get; set; } = 10012;
        public string HexagramCentralServerApplicationName { get; set; } = "HexagramCentral";
        public int SetupConnectionDelay { get; set; } = 6000;

        public string DatabaseServerAddress { get; set; } = "127.0.0.1";
        public int DatabasePort { get; set; } = 10000;
        public string DatabaseUsername { get; set; } = "";
        public string DatabasePassword { get; set; } = "";
        public string DatabasePrefix { get; set; } = "DS.Dev";
        public string DatabaseCharset { get; set; } = "utf8mb4";
    }
}
