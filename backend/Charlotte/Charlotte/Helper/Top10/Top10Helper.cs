using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.Shared;
using Charlotte.Interface.Top10;
using Charlotte.VModel.Top10;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Charlotte.Helper.Top10
{
    public class Top10Helper : ITop10Helper
    {
        public async Task<List<Top10Model>> GetAsync()
        {
            using (var db = new CharlotteContext())
            {
                var result = await db.OrderDetail.GroupBy(a => new { a.ProductId }).Select(a=> new 
                {
                    a.Key.ProductId,
                    SellCount = a.Sum(a=> a.ProductAmount)
                })
                .OrderByDescending(a=> a.SellCount).Take(10)
                .Join(db.ProductInformation, a=> a.ProductId, b=> b.ProductId, (a, b) => new Top10Model
                {
                    ProductId = a.ProductId,
                    ProductName = b.ProductName,
                    SellPrice = b.SellPrice,
                    Inventory = b.Inventory,
                    ProductImgPath = b.ProductImgPath
                }).ToListAsync();
                return result;
            }
        }
    }
}
