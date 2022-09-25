using Charlotte.Enum;
using Charlotte.Interface.Shared;
using Charlotte.Services;
using Charlotte.VModel.ManagerMenu;
using Dapper;
using System.Data.SqlClient;

namespace Charlotte.Helper.ManagerMenu
{
    public class ManagerMenuHelper: IGetAsync<List<ManagerMenuVModel>>
    {
        public async Task<List<ManagerMenuVModel>> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"select router.Link, router.RouterName, router.GroupId, router.Icon
                                from ManagerMain as main
                                left join ManagerRole as role on role.RoleId = main.RoleId
                                left join ManagerRoleAuth as auth on auth.RoleId = role.RoleId
                                left join Router as router on auth.RouterId = router.RouterId
                                where main.ManagerUserId = @id and router.Flag = 'Y' and auth.ViewAuth = 'Y'
                                order by router.Sort";
                var result = await con.QueryAsync<ManagerMenuVModel>(sqlStr, new { id });
                return result.ToList();
            }
        }
    }
}
