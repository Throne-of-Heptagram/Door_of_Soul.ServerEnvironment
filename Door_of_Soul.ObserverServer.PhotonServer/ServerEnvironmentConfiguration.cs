namespace Door_of_Soul.ObserverServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public string HexagramEntranceServerAddress { get; set; } = "127.0.0.1";
        public int HexagramEntranceServerPort { get; set; } = 10027;
        public string HexagramEntranceServerApplicationName { get; set; } = "HexagramEntrance";
    }
}
