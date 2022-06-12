using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.ManagerOrder;
using Charlotte.Interface.Shared;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Charlotte.Helper.ManagerOrder
{
    public class ManagerOrderHelper: IManagerOrderHelper
    {
        public async Task<TableVModel<ManagerOrderVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.Order.Join(db.UserMain, a => a.UserId, b => b.UserId, (a, b) => new
                { // 客戶表
                    OrderId = a.OrderId,
                    UserId = a.UserId,
                    UserName = b.UserName,
                    CreateDate = a.CreatedDate,
                    ModifyDate = a.ModifyDate
                }).Join(db.OrderDetail, a => a.OrderId, b => b.OrderId, (a, b) => new
                { // 訂單明細
                    OrderId = a.OrderId,
                    UserId = a.UserId,
                    UserName = a.UserName,
                    CreateDate = a.CreateDate,
                    ModifyDate = a.ModifyDate,
                    OrderDetail = b
                }).GroupBy(a => new { a.OrderId, a.UserId, a.UserName, a.CreateDate, a.ModifyDate }).Select(a => new ManagerOrderVModel
                {
                    OrderId = a.Key.OrderId,
                    UserId = a.Key.UserId,
                    UserName = a.Key.UserName,
                    CreateDate = a.Key.CreateDate.ToString("yyyy-MM-dd"),
                    ModifyDate = a.Key.ModifyDate.HasValue ? a.Key.ModifyDate.Value.ToString("yyyy-MM-dd") : "",
                    OrderAmount = a.Sum(a => a.OrderDetail.ProductAmount * a.OrderDetail.ProductPrice)
                }).AsQueryable();
                if (filterStr != null) 
                {
                    query = query.Where(a => a.UserName.Contains(filterStr) ||
                                            a.OrderId.ToString().Contains(filterStr) ||
                                            a.CreateDate.Contains(filterStr) ||
                                            a.ModifyDate.Contains(filterStr)
                                       );
                }
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.ToListAsync();
                return new TableVModel<ManagerOrderVModel>(result, tableTotalCount);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var detailData = await db.OrderDetail.Where(a => a.OrderId == id).ToListAsync();
                var orderData = await db.Order.SingleAsync(a => a.OrderId == id);
                db.OrderDetail.RemoveRange(detailData);
                db.Order.Remove(orderData);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var detailData = await db.OrderDetail.Where(a => idList.Contains(a.OrderId)).ToListAsync();
                var orderData = await db.Order.Where(a => idList.Contains(a.OrderId)).ToListAsync();
                db.OrderDetail.RemoveRange(detailData);
                db.Order.RemoveRange(orderData);
                await db.SaveChangesAsync();
            } 
        }
    }
}
