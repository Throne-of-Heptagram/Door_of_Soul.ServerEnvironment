namespace Door_of_Soul.HexagramLoveServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public string HexagramCentralServerAddress { get; set; } = "127.0.0.1";

        public int HexagramCentralServerPort { get; set; } = 10005;

        public string HexagramCentralServerApplicationName { get; set; } = "HexagramCentral";

        public int SetupConnectionDelay { get; set; } = 2500;
    }
}
