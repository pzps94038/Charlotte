using Charlotte.DataBase.DbContextModel;
using Charlotte.Enum;
using Charlotte.Interface.ManagerOrderDetail;
using Charlotte.Model.ManagerOrderDetail;
using Charlotte.Services;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.ManagerOrderDetail;
using Dapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Charlotte.Helper.ManagerOrderDetail
{
    public class ManagerOrderDetailHelper : IManagerOrderDetailHelper
    {
        public async Task DeleteAsync(int orderId, int productId)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.OrderDetail.SingleAsync(a => a.OrderId == orderId && a.ProductId == productId);
                db.OrderDetail.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<ManagerOrderDetailVModel>> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT [ProductId]
                                ,[ProductAmount]
                                ,[ProductPrice]
                                FROM [Charlotte].[dbo].[OrderDetail]
                                Where OrderId = @OrderId";
                var result = await con.QueryAsync<ManagerOrderDetailVModel>(sqlStr, new { OrderId = id });
                return result.ToList();
            }
        }

        public async Task ModifyAsync(int id, List<ManagerOrderDetailModel> request)
        {
            using (var db = new CharlotteContext())
            {
                var detailData = await db.OrderDetail.Where(a => a.OrderId == id).ToListAsync();
                var now = DateTime.Now;
                foreach (var data in detailData) 
                {
                    var modifyData = request.Single(a => a.ProductId == data.ProductId);
                    data.ProductPrice = modifyData.ProductPrice;
                    data.ProductAmount = modifyData.ProductAmount;
                    data.ModifyDate = now;
                }
                await db.SaveChangesAsync();
            }
        }
    }
}
