namespace Charlotte.Helper
{
    public static class GetConnectionStringHelper
    {
        public static string GetConnectionString(string ConnectionStringName) 
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString(ConnectionStringName);
        }
    }
}
