using Charlotte.Database.Model;
using Charlotte.DataBase.DbContextModel;
using Charlotte.Model.ManagerRouter;
using Charlotte.Services;
using Charlotte.VModel.ManagerRouter;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.ManagerRouter
{
    public static class ManagerRouterHelper
    {
        /// <summary>
        /// 取得所有路由表
        /// </summary>
        /// <returns>所有路由表</returns>
        public async static Task<List<ManagerRouterVModel>> GetRouters()
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select RouterId as routerId, Link as link, Icon as icon, GroupId as groupId, Flag as flag, RouterName as routerName
                                from Router as router";
                var result = await con.QueryAsync<ManagerRouterVModel>(sqlStr);
                return result.ToList();
            }
        }

        /// <summary>
        /// 創建路由
        /// </summary>
        /// <param name="rq">路由參數</param>
        /// <returns></returns>
        public async static Task CreateRouter(ManagerRouterModel rq) 
        {
            using (var db = new CharlotteContext()) 
            {
                var data = new Router();
                data.Flag = rq.flag ? "Y" : "N";
                data.Icon = rq.icon == null ? null : rq.icon;
                data.Link = rq.link;
                data.RouterName = rq.routerName;
                data.GroupId = rq.groupId;
                db.Router.Add(data);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改路由
        /// </summary>
        /// <param name="routerId">路由ID</param>
        /// <param name="rq">路由參數</param>
        /// <returns></returns>
        public async static Task ModifyRouter(int routerId, ManagerRouterModel rq)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Router.SingleAsync(a=> a.RouterId == routerId);
                data.RouterName = rq.routerName;
                data.Flag = rq.flag ? "Y" : "N";
                data.Icon = rq.icon == null ? null : rq.icon;
                data.Link = rq.link;
                data.GroupId = rq.groupId;
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 刪除路由
        /// </summary>
        /// <param name="routerId">要刪除的路由ID</param>
        /// <returns></returns>

        public async static Task DeleteRouter(int routerId) 
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.Router.SingleAsync(a => a.RouterId == routerId);
                db.Router.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 批次刪除路由
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        public async static Task BatchDeleteRouter(List<int> rq)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.Router.Where(a => rq.Contains(a.RouterId)).ToListAsync();
                db.Router.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }
    }
}
