using Charlotte.DataBase.DbContextModel;
using Charlotte.Enum;
using Charlotte.Interface.Shared;
using Charlotte.Interface.Top10;
using Charlotte.Services;
using Charlotte.VModel.Top10;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Linq;

namespace Charlotte.Helper.Top10
{
    public class Top10Helper : ITop10Helper
    {
        public async Task<List<Top10Model>> GetAsync()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.CharlotteRedis));
            using (var redisDbcon = await ConnectionMultiplexer.ConnectAsync(sqlConStr))
            {
                var redisDb = redisDbcon.GetDatabase();
                var redisValue = await redisDb.StringGetAsync("Top10");
                TimeSpan? expiry = new TimeSpan(0, 30, 0); // 30min過期
                // 如果redisMiss 去db取
                if (redisValue.IsNullOrEmpty)
                {
                    using (var db = new CharlotteContext())
                    {
                        var result = await db.OrderDetail.GroupBy(a => new { a.ProductId }).Select(a => new
                        {
                            a.Key.ProductId,
                            SellCount = a.Sum(a => a.ProductAmount)
                        })
                        .OrderByDescending(a => a.SellCount).Take(10)
                        .Join(db.ProductInformation, a => a.ProductId, b => b.ProductId, (a, b) => new Top10Model
                        {
                            ProductId = a.ProductId,
                            ProductName = b.ProductName,
                            ProductImgPath = b.ProductImgPath
                        }).ToListAsync();
                        // 取完資料給放在redis裡面
                        await redisDb.StringSetAsync("Top10", JsonConvert.SerializeObject(result), expiry);
                        return result;
                    }
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Top10Model>>(redisValue!)!;
                }
            }
        }
    }
}
