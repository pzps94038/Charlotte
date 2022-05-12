using Microsoft.Extensions.Configuration;

namespace Charlotte.Services
{
    public class GetAppSettingsUtils
    {
        private readonly IConfiguration configruration;

        public GetAppSettingsUtils(IConfiguration configruration)
        {
            this.configruration = configruration;
        }

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

        /// <summary>
        /// DI注入取設定
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSetting(string key) 
        {
            return configruration[key];
        }
    }
}
