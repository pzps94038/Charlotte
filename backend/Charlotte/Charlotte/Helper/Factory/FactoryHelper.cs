using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.Shared;
using Charlotte.Services;
using Charlotte.VModel.Factory;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.Factory
{
    public class FactoryHelper: ICRUDAsyncHelper<FactoryVModel, FactoryVModel, string, string>
    {

        public async Task CreateAsync(string factoryName)
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

        public async Task ModifyAsync(int id, string request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Factory.SingleAsync(a => a.FactoryId == id);
                data.FactoryName = request;
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Factory.SingleAsync(a => a.FactoryId == id);
                db.Factory.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.Factory.Where(a => idList.Contains(a.FactoryId)).ToListAsync();
                db.Factory.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<FactoryVModel>> GetAllAsync()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT [FactoryId]
                                ,[FactoryName]
                                ,convert(varchar, CreateDate, 23) as CreateDate
                                ,convert(varchar, ModifyDate, 23) as ModifyDate
                                  FROM [Charlotte].[dbo].[Factory]";
                var result = await con.QueryAsync<FactoryVModel>(sqlStr);
                return result.ToList();
            }
        }

        public async Task<FactoryVModel> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT [FactoryId]
                                ,[FactoryName]
                                ,convert(varchar, CreateDate, 23) as CreateDate
                                ,convert(varchar, ModifyDate, 23) as ModifyDate
                                FROM [Charlotte].[dbo].[Factory]
                                Where FactoryId = @FactoryId";
                var result = await con.QueryFirstOrDefaultAsync<FactoryVModel>(sqlStr, new { factoryId = id });
                return result;
            }
        }
    }
}
