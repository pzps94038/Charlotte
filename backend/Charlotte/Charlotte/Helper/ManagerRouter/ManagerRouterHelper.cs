using Charlotte.Database.Model;
using Charlotte.DataBase.DbContextModel;
using Charlotte.Enum;
using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerRouter;
using Charlotte.Services;
using Charlotte.VModel.ManagerRouter;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;
using System.Linq.Dynamic.Core;
using System.Linq;
using Mapster;
using Charlotte.VModel.Shared;

namespace Charlotte.Helper.ManagerRouter
{
    public class ManagerRouterHelper: ICRUDAsyncHelper<TableVModel<ManagerRouterVModel>, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel>
    {
        public async Task CreateAsync(ManagerRouterModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = new Router();
                data.Flag = request.Flag ? "Y" : "N";
                data.Icon = request.Icon == null ? null : request.Icon;
                data.Link = request.Link;
                data.RouterName = request.RouterName;
                data.GroupId = request.GroupId;
                data.Sort = request.Sort;
                data.CreateDate = DateTime.Now;
                db.Router.Add(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ManagerRouterModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Router.SingleAsync(a => a.RouterId == id);
                data.RouterName = request.RouterName;
                data.Flag = request.Flag ? "Y" : "N";
                data.Icon = request.Icon == null ? null : request.Icon;
                data.Link = request.Link;
                data.GroupId = request.GroupId;
                data.Sort = request.Sort;
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Router.SingleAsync(a => a.RouterId == id);
                db.Router.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.Router.Where(a => idList.Contains(a.RouterId)).ToListAsync();
                db.Router.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task<TableVModel<ManagerRouterVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.Router.AsQueryable();
                if (filterStr != null) 
                {
                    query = query.Where(a => a.RouterId.ToString().Contains(filterStr) ||
                                                    a.RouterName.Contains(filterStr) ||
                                                    a.Link.Contains(filterStr) ||
                                                    (a.Icon != null && a.Icon.Contains(filterStr)) ||
                                                    a.GroupId.ToString().Contains(filterStr) ||
                                                    a.Sort.ToString().Contains(filterStr) ||
                                                    a.Flag.Contains(filterStr)) ; ;
                }
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.ToListAsync();
                return new TableVModel<ManagerRouterVModel>(result.Adapt<List<ManagerRouterVModel>>(), tableTotalCount);
            }
        }

        public async Task<ManagerRouterVModel> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select RouterId, Link, Icon, GroupId, Flag, RouterName, Sort
                                from Router as router
                                where RouterId = @id";
                var result = await con.QueryFirstAsync<ManagerRouterVModel>(sqlStr, new{ id });
                return result;
            }
        }
    }
}
