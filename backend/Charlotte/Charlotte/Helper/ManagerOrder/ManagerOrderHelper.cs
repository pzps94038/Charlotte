using Charlotte.DataBase.DbContextModel;
using Charlotte.Enum;
using Charlotte.Interface.ManagerOrder;
using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerOrder;
using Charlotte.Model.ManagerOrderDetail;
using Charlotte.Services;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Dynamic.Core;
namespace Charlotte.Helper.ManagerOrder
{
    public class ManagerOrderHelper: IManagerOrderHelper
    {
        public async Task<TableVModel<ManagerOrderVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (var db = new CharlotteContext())
            {
                var group = db.Order.Join(db.UserMain, a => a.UserId, b => b.UserId, (a, b) => new {
                    a.OrderId,
                    b.UserId,
                    b.UserName,
                    a.CreateDate,
                    a.ModifyDate
                }).Join(db.OrderDetail, a => a.OrderId, b => b.OrderId, (a, b) => new {
                    a.OrderId,
                    a.UserId,
                    a.UserName,
                    a.CreateDate,
                    a.ModifyDate,
                    b.ProductAmount,
                    b.ProductPrice
                }).GroupBy(a => new { a.UserId, a.UserName, a.OrderId, a.CreateDate, a.ModifyDate })
                .AsQueryable();
                // 處理搜尋條件
                if (filterStr != null) {
                    group = group.Where(a => a.Key.UserId.ToString().Contains(filterStr) ||
                                            a.Key.UserName.Contains(filterStr) ||
                                            (a.Sum(b=> b.ProductPrice * b.ProductAmount)).ToString().Contains(filterStr));
                }

                // 改成對應的回傳的對應參數，來做動態order
                var query = group.Select(a => new {
                    UserId = a.Key.UserId,
                    UserName = a.Key.UserName,
                    OrderId = a.Key.OrderId,
                    CreateDate = a.Key.CreateDate,
                    ModifyDate = a.Key.ModifyDate,
                    OrderAmount = a.Sum(b => b.ProductAmount * b.ProductPrice)
                });

                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");

                // 符合資料總數
                var count = query.Count();

                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);

                var result = await query.Select(a => new ManagerOrderVModel {
                                        UserId = a.UserId,
                                        UserName = a.UserName,
                                        OrderId = a.OrderId,
                                        CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                                        ModifyDate = a.ModifyDate.HasValue ? a.ModifyDate.Value.ToString("yyyy-MM-dd") : "",
                                        OrderAmount = a.OrderAmount
                                  })
                                  .ToListAsync();
                return new TableVModel<ManagerOrderVModel>(result, query.Count());
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

        public async Task CreateAsync(ManagerOrderModel request)
        {
            using (var db = new CharlotteContext())
            {
                using (var transcation = await db.Database.BeginTransactionAsync()) 
                {
                    try
                    {
                        var orderId = await this.AddOrder(db, request.UserId);
                        await this.AddOrderDetail(db, orderId, request.OrderDetail);
                        await transcation.CommitAsync();
                    }
                    catch
                    {
                        await transcation.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        private async Task<int> AddOrder(CharlotteContext db, int userId) 
        {
            var order = new Database.Model.Order();
            order.UserId = userId;
            order.CreateDate = DateTime.Now;
            await db.Order.AddAsync(order);
            await db.SaveChangesAsync();
            return order.OrderId;
        }

        private async Task AddOrderDetail(CharlotteContext db, int orderId, List<ManagerOrderDetailModel> orderDetail) 
        {
            DataBase.Model.OrderDetail item;
            List<DataBase.Model.OrderDetail> itemList = new List<DataBase.Model.OrderDetail>();
            foreach (var product in orderDetail) 
            {
                item = new DataBase.Model.OrderDetail();
                item.OrderId = orderId;
                item.ProductId = product.ProductId;
                item.ProductAmount = product.ProductAmount;
                item.ProductPrice = product.ProductPrice;
                item.CreateDate = DateTime.Now;
                itemList.Add(item);
            }
            await db.OrderDetail.AddRangeAsync(itemList);
            await db.SaveChangesAsync();
        }

        public async Task ModifyAsync(int id, ManagerOrderModel request)
        {
            using (var db = new CharlotteContext())
            {
                var order = db.Order.Single(a => a.OrderId == id);
                order.ModifyDate = DateTime.Now;
                order.UserId = request.UserId;
                // 舊資料先查詢存在記憶體
                var oldOrderDetail = await db.OrderDetail.Where(a => a.OrderId == id).ToListAsync();
                // 資料庫移除舊資料
                db.OrderDetail.RemoveRange(oldOrderDetail);
                DataBase.Model.OrderDetail item;
                List<DataBase.Model.OrderDetail> itemList = new List<DataBase.Model.OrderDetail>();
                foreach (var product in request.OrderDetail)
                {
                    var oldProduct = oldOrderDetail.FirstOrDefault(a => a.ProductId == product.ProductId);
                    item = new DataBase.Model.OrderDetail();
                    item.OrderId = id;
                    item.ProductId = product.ProductId;
                    item.ProductAmount = product.ProductAmount;
                    item.ProductPrice = product.ProductPrice;
                    item.CreateDate = oldProduct == null ? DateTime.Now : oldProduct.CreateDate;
                    item.ModifyDate = DateTime.Now;
                    itemList.Add(item);
                }
                await db.OrderDetail.AddRangeAsync(itemList);
                await db.SaveChangesAsync();
            }
        }
    }
}
