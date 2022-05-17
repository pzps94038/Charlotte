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

namespace Charlotte.Helper.ManagerRouter
{
    public class ManagerRouterHelper: ICRUDAsyncHelper<ManagerRouterVModel, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel>
    {
        public async Task CreateAsync(ManagerRouterModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = new Router();
                data.Flag = request.flag ? "Y" : "N";
                data.Icon = request.icon == null ? null : request.icon;
                data.Link = request.link;
                data.RouterName = request.routerName;
                data.GroupId = request.groupId;
                db.Router.Add(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ManagerRouterModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Router.SingleAsync(a => a.RouterId == id);
                data.RouterName = request.routerName;
                data.Flag = request.flag ? "Y" : "N";
                data.Icon = request.icon == null ? null : request.icon;
                data.Link = request.link;
                data.GroupId = request.groupId;
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

        public async Task<List<ManagerRouterVModel>> GetAllAsync()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select RouterId, Link, Icon, GroupId, Flag, RouterName
                                from Router as router";
                var result = await con.QueryAsync<ManagerRouterVModel>(sqlStr);
                return result.ToList();
            }
        }

        public async Task<ManagerRouterVModel> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select RouterId, Link, Icon, GroupId, Flag, RouterName
                                from Router as router
                                where RouterId = @id";
                var result = await con.QueryFirstAsync<ManagerRouterVModel>(sqlStr, new{ id });
                return result;
            }
        }
    }
}
