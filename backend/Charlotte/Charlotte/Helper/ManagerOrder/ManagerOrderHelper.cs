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
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT o.OrderId, u.UserId, u.UserName, convert(varchar, o.CreateDate, 23) as CreateDate, convert(varchar, o.ModifyDate, 23) as ModifyDate, SUM(d.ProductAmount * d.ProductPrice) as OrderAmount
                                FROM [Charlotte].[dbo].[Order] as o
                                left join [dbo].[UserMain] as u on u.UserId = o.UserId
                                left join [dbo].[OrderDetail] as d on d.OrderId = o.OrderId
                                group by o.OrderId, u.UserId, u.UserName, o.CreateDate, o.ModifyDate ";
                string countStr = @"Select COUNT(*)
                                    FROM [Charlotte].[dbo].[Order] as o
                                    left join [dbo].[UserMain] as u on u.UserId = o.UserId
                                    left join [dbo].[OrderDetail] as d on d.OrderId = o.OrderId
                                    group by o.OrderId, u.UserId, u.UserName, o.CreateDate, o.ModifyDate ";
                var sqlParams = new DynamicParameters();
                if (filterStr != null)
                {
                    string condition = @"HAVING o.OrderId like @filterStr or u.UserId like @filterStr or u.UserName like @filterStr or SUM(d.ProductAmount * d.ProductPrice) like @filterStr ";
                    sqlStr += condition;
                    countStr += condition;
                    sqlParams.Add("filterStr", "%" + filterStr + "%");
                }
                var tableTotalCount = await con.QueryAsync<int>(countStr, sqlParams);
                if (orderBy != null && orderDescription != null)
                {
                    sqlStr += @"order by 
                                CASE WHEN @orderBy = 'orderId asc' THEN o.OrderId END ASC,
                                CASE WHEN @orderBy = 'orderId desc' THEN o.OrderId END DESC,
                                CASE WHEN @orderBy = 'userId asc' THEN u.UserId END ASC,
                                CASE WHEN @orderBy = 'userId desc' THEN u.UserId END DESC,
                                CASE WHEN @orderBy = 'userName asc' THEN u.UserName END ASC,
                                CASE WHEN @orderBy = 'userName desc' THEN u.UserName END DESC,
                                CASE WHEN @orderBy = 'createDate asc' THEN o.CreateDate END ASC,
                                CASE WHEN @orderBy = 'createDate desc' THEN o.CreateDate END DESC,
                                CASE WHEN @orderBy = 'modifyDate asc' THEN o.ModifyDate END ASC,
                                CASE WHEN @orderBy = 'modifyDate desc' THEN o.ModifyDate END DESC,
                                CASE WHEN @orderBy = 'orderAmount asc' THEN SUM(d.ProductPrice * d.ProductAmount) END ASC,
                                CASE WHEN @orderBy = 'orderAmount desc' THEN SUM(d.ProductPrice * d.ProductAmount) END DESC ";
                    sqlParams.Add("orderBy", orderDescription == "desc" ? $"{orderBy} desc" : $"{orderBy} asc");
                }
                else 
                    sqlStr += @"order by o.OrderId ";
                if (limit != null && offset != null) 
                {
                    sqlStr += @"OFFSET @offset ROWS
                                FETCH NEXT @limit ROWS ONLY";
                    sqlParams.Add("@offset", (int)offset);
                    sqlParams.Add("@limit", (int)limit);
                }
                var query = await con.QueryAsync<ManagerOrderVModel>(sqlStr, sqlParams);
                var result = query.ToList();
                return new TableVModel<ManagerOrderVModel>(result, tableTotalCount.Count());
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
