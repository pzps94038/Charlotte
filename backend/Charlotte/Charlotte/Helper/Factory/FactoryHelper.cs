using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.Shared;
using Charlotte.Services;
using Charlotte.VModel.Factory;
using Charlotte.VModel.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;
using System.Linq.Dynamic.Core;
using Mapster;

namespace Charlotte.Helper.Factory
{
    public class FactoryHelper: ICRUDAsyncHelper<TableVModel<FactoryVModel>, FactoryVModel, string, string>
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

        public async Task<TableVModel<FactoryVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                IQueryable<Database.Model.Factory> filterResult = db.Factory;
                if (filterStr != null)
                {
                    filterResult = filterResult.Where(a => a.FactoryId.ToString().Contains(filterStr) ||
                                                      a.FactoryName.Contains(filterStr));
                }
                var tableTotalCount = filterResult.Count();
                if (orderBy != null && orderDescription != null)
                    filterResult = orderDescription == "desc" ? filterResult.OrderBy($"{orderBy} desc") : filterResult.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    filterResult = filterResult.Skip((int)offset).Take((int)limit);
                var result = await filterResult.Select(a=> new 
                    {
                        FactoryId = a.FactoryId,
                        FactoryName = a.FactoryName,
                        CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                        ModifyDate = a.ModifyDate.HasValue ? a.ModifyDate.Value.ToString("yyyy-MM-dd") : "",
                    }).ToListAsync();
                return new TableVModel<FactoryVModel>(result.Adapt<List<FactoryVModel>>(), tableTotalCount);
            }
        }
    }
}
