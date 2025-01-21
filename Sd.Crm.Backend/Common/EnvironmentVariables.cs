namespace Sd.Crm.Backend.Common
{
    public static class EnvironmentVariables
    {
        public const string ENVIRONMENT = nameof(ENVIRONMENT);

        public const string APP_VERSION = nameof(APP_VERSION);

        public static class Database
        {
            public const string DATABASE_HOST = nameof(DATABASE_HOST);
            public const string DATABASE_PORT = nameof(DATABASE_PORT);
            public const string DATABASE_NAME = nameof(DATABASE_NAME);
            public const string DATABASE_LOGIN = nameof(DATABASE_LOGIN);
            public const string DATABASE_PASSWORD = nameof(DATABASE_PASSWORD);
        }
    }

}
