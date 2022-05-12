using Charlotte.Enum;
using Charlotte.Services;
using Charlotte.VModel.ManagerMenu;
using Dapper;
using System.Data.SqlClient;

namespace Charlotte.Helper.ManagerMenu
{
    public class ManagerMenuHelper: IManagerMenuHelper
    {
        public async Task<List<ManagerMenuVModel>> GetMenu(int userId) 
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select router.Link as link, router.RouterName as routerName, router.GroupId as groupId, router.Icon as icon
                                from ManagerMain as main
                                left join ManagerRole as role on role.RoleId = main.RoleId
                                left join ManagerRoleAuth as auth on auth.RoleId = role.RoleId
                                left join Router as router on auth.RouterId = router.RouterId
                                where main.ManagerUserId = @userId and router.Flag = 'Y' and auth.ViewAuth = 'Y'";
                var result = await con.QueryAsync<ManagerMenuVModel>(sqlStr, new { userId });
                return result.ToList();
            }
        }
    }
}
