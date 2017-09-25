using System.Collections.Generic;
using Door_of_Soul.Communication.HexagramEntranceServer;
using System;

namespace Door_of_Soul.HexagramEntranceServer.PhotonServer
{
    public class ServerEnvironmentConfiguration
    {
        public static ServerEnvironmentConfiguration Instance { get; private set; }
        public static void Initialize(ServerEnvironmentConfiguration instance)
        {
            Instance = instance;
        }

        public int HexagramEntranceId { get; set; } = 1;

        public int LoginServerListenPort { get; set; } = 10025;
        public int TrinityServerListenPort { get; set; } = 10026;
        public int ObserverServerListenPort { get; set; } = 10027;

        public string[] HexagramNodeServerAddresses { get; set; } = new string[12];
        public int[] HexagramNodeServerPorts { get; set; } = new int[12];
        public string[] HexagramNodeServerApplicationNames { get; set; } = new string[12];

        public int HexagramNodeServerReconnectDelayMillisecond { get; set; } = 10000;

        public string DatabaseServerAddress { get; set; } = "127.0.0.1";
        public int DatabasePort { get; set; } = 10000;
        public string DatabaseUsername { get; set; } = "";
        public string DatabasePassword { get; set; } = "";
        public string DatabasePrefix { get; set; } = "DS.Dev";
        public string DatabaseCharset { get; set; } = "utf8mb4";
    }
}
