using Charlotte.DataBase.DbContextModel;
using Charlotte.Model.ManagerRoleAuth;
using Charlotte.Services;
using Charlotte.VModel.ManagerRoleRouter;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.ManagerRoleAuth
{
    public static class ManagerRoleAuthHeper
    {

        public static async Task<List<ManagerRoleAuthVModel>> GetManagerRoleRouters(int roleId) 
        {
            var result = new List<ManagerRoleAuthVModel>();
            using (var db = new CharlotteContext()) 
            {
                var roleData = await db.ManagerRole.SingleAsync(a=> a.RoleId == roleId);
                var routers = await db.Router.ToListAsync();
                var roleAuthList = await db.ManagerRoleAuth.Where(a => a.RoleId == roleId).ToListAsync();
                foreach (var router in routers)
                {
                    var authData = roleAuthList.SingleOrDefault(a => a.RouterId == router.RouterId);
                    // 檢查權限表有沒有資料沒有資料預設給false
                    if (authData == null)
                        result.Add(new ManagerRoleAuthVModel(roleId, router.RouterId, router.RouterName));
                    else
                        result.Add(new ManagerRoleAuthVModel(authData, router.RouterName));
                }
            }
            return result;
        }

        public static async Task CreateOrUpdateRoleAuth(int roleId, List<ManagerRoleAuthModel> req)
        {
            using (var db = new CharlotteContext())
            {
                var existAuthData = await db.ManagerRoleAuth.Where(a => a.RoleId == roleId).ToListAsync();
                foreach (var router in req) 
                {
                    var authData = existAuthData.FirstOrDefault(a => a.RouterId == router.routerId);
                    if (authData == null)
                    {
                        db.ManagerRoleAuth.Add(new Database.Model.ManagerRoleAuth 
                        {
                            RoleId = roleId,
                            RouterId = router.routerId,
                            ViewAuth = router.viewAuth ? "Y" : "N",
                            CreateAuth = router.createAuth ? "Y" : "N",
                            ModifyAuth = router.modifyAuth ? "Y" : "N",
                            DeleteAuth = router.deleteAuth ? "Y" : "N",
                            ExportAuth = router.exportAuth ? "Y" : "N",
                            CreateDate = DateTime.Now
                        });
                    }
                    else 
                    {
                        authData.ViewAuth = router.viewAuth ? "Y" : "N";
                        authData.CreateAuth = router.createAuth ? "Y" : "N";
                        authData.ModifyAuth = router.modifyAuth ? "Y" : "N";
                        authData.DeleteAuth = router.deleteAuth ? "Y" : "N";
                        authData.ExportAuth = router.exportAuth ? "Y" : "N";
                        authData.ModifyDate = DateTime.Now;
                    }
                }
                await db.SaveChangesAsync();
            }
        }

        public static async Task<CheckManagerRoleAuthVModel<bool>> CheckRoleAuth(int userId, string routerPath)
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            CheckManagerRoleAuthVModel<bool> result = new CheckManagerRoleAuthVModel<bool>();
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select router.RouterName as routerName
                                  ,[ViewAuth] as viewAuth
                                  ,[CreateAuth] as createAuth
                                  ,[ModifyAuth] as modifyAuth
                                  ,[DeleteAuth] as deleteAuth
                                  ,[ExportAuth] as exportAuth
                                  from [Charlotte].[dbo].[ManagerRoleAuth] as auth
                                  left join Router as router on router.RouterId = auth.RouterId
                                  left join ManagerMain as main on main.RoleId = auth.RoleId
                                  where router.Link = @routerPath and main.ManagerUserId = @userId";
                // 檢查路由權限表，如果有資料就拿，沒有就給預設值為false
                var data = await con.QueryFirstOrDefaultAsync<CheckManagerRoleAuthVModel<string>>(sqlStr, new { userId, routerPath });
                if (data == null)
                {
                    result.viewAuth = false;
                    result.createAuth = false;
                    result.modifyAuth = false;
                    result.deleteAuth = false;
                    result.exportAuth = false;
                }
                else 
                {
                    result.viewAuth = data.viewAuth == "Y" ? true : false;
                    result.createAuth = data.createAuth == "Y" ? true : false;
                    result.modifyAuth = data.modifyAuth == "Y" ? true : false;
                    result.deleteAuth = data.deleteAuth == "Y" ? true : false;
                    result.exportAuth = data.exportAuth == "Y" ? true : false;
                }

                return result;
            }
        }
    }
}
