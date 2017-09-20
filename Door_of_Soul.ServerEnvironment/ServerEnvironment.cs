namespace Door_of_Soul.ServerEnvironment
{
    public abstract class ServerEnvironment
    {
        public static ServerEnvironment Instance { get; private set; }
        public static void Initialize(ServerEnvironment instance)
        {
            Instance = instance;
        }

        public bool Setup(out string errorMessage)
        {
            if(!SetupLog(out errorMessage))
            {
                return false;
            }
            if (!SetupConfiguration(out errorMessage))
            {
                return false;
            }
            if (!SetupDatabase(out errorMessage))
            {
                return false;
            }
            if (!SetupCommunication(out errorMessage))
            {
                return false;
            }
            if (!SetupServer(out errorMessage))
            {
                return false;
            }
            return true;
        }
        public abstract void TearDown();

        public abstract bool SetupLog(out string errorMessage);
        public abstract bool SetupConfiguration(out string errorMessage);
        public abstract bool SetupDatabase(out string errorMessage);
        public abstract bool SetupCommunication(out string errorMessage);
        public abstract bool SetupServer(out string errorMessage);
    }
}
