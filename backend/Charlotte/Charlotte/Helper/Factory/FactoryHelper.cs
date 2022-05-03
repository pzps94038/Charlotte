using Charlotte.DataBase.DbContextModel;
using Charlotte.Services;
using Charlotte.VModel.Factory;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.Factory
{
    public static class FactoryHelper
    {
        /// <summary>
        /// 取得所有廠商資料
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FactoryVModel>> GetFactorys() 
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT [FactoryId] as factoryId
                                ,[FactoryName] as factoryName
                                ,convert(varchar, CreateDate, 23) as createDate
                                ,convert(varchar, ModifyDate, 23) as modifyDate
                                  FROM [Charlotte].[dbo].[Factory]";
                var result = await con.QueryAsync<FactoryVModel>(sqlStr);
                return result.ToList();
            }
        }

        /// <summary>
        /// 取得單個廠商資料
        /// </summary>
        /// <returns></returns>
        public static async Task<FactoryVModel> GetFactory(int factoryId)
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT [FactoryId] as factoryId
                                ,[FactoryName] as factoryName
                                ,convert(varchar, CreateDate, 23) as createDate
                                ,convert(varchar, ModifyDate, 23) as modifyDate,
                                FROM [Charlotte].[dbo].[Factory]
                                Where factoryId = @factoryId";
                var result = await con.QueryFirstOrDefaultAsync<FactoryVModel>(sqlStr, new { factoryId });
                return result;
            }
        }

        /// <summary>
        /// 新增廠商
        /// </summary>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public static async Task CreateFactory(string factoryName)
        {
            using (var db = new CharlotteContext()) 
            {
                var data = new Database.Model.Factory();
                data.CreateDate = DateTime.Now;
                data.FactoryName = factoryName;
                db.Factory.Add(data);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 修改廠商
        /// </summary>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public static async Task ModifyFactory(int factoryId, string factoryName)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Factory.SingleAsync(a => a.FactoryId == factoryId);
                data.FactoryName = factoryName;
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 刪除廠商
        /// </summary>
        /// <param name="factoryId">廠商Id</param>
        /// <returns></returns>

        public static async Task DeleteFactory(int factoryId) 
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Factory.SingleAsync(a=> a.FactoryId == factoryId);
                db.Factory.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 批次刪除廠商
        /// </summary>
        /// <param name="factorysId">多個廠商Id</param>
        /// <returns></returns>
        public static async Task BatchDeleteFactory(List<int> factorysId)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.Factory.Where(a=> factorysId.Contains(a.FactoryId)).ToListAsync();
                db.Factory.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }
    }
}
