namespace CharlotteBackendManagement.Helper
{
    public static class GetAppSettingsHelper
    {
        /// <summary>
        /// 取AppSetting連線字串
        /// </summary>
        /// <param name="ConnectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string ConnectionStringName) 
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString(ConnectionStringName);
        }
        /// <summary>
        /// 取AppSetting設定
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="childKey"></param>
        /// <returns></returns>
        public static string GetAppSettingsValue(string Key, string childKey) 
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config[$"{Key}:{childKey}"];
        }
    }
}
