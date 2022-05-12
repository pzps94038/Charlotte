using Charlotte.DataBase.DbContextModel;
using Charlotte.Model.ManagerRole;
using Charlotte.Database.Model;
using System.Data.SqlClient;
using Charlotte.Services;
using Charlotte.VModel.ManagerRole;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Charlotte.CustomizeException.CustomizeException;
using Charlotte.Enum;

namespace Charlotte.Helper.ManagerRole
{
    public class ManagerRoleHelper: IManagerRoleHelper
    {
        /// <summary>
        /// 創建角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task CreateManagerRole(ManagerRoleModel req) 
        {
            using (var db = new CharlotteContext()) 
            {
               var role = new Database.Model.ManagerRole();
               role.RoleName = req.roleName;
               role.CreateDate = DateTime.Now;
                db.ManagerRole.Add(role);
               await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 取得所有角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<ManagerRoleVModel>> GetRoles()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT RoleId as roleId, RoleName as roleName,  convert(varchar, CreateDate, 23) as createDate, convert(varchar, ModifyDate, 23) as modifyDate
                                FROM ManagerRole";
                var result = await con.QueryAsync<ManagerRoleVModel>(sqlStr);
                return result.ToList();
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="req">角色資訊</param>
        /// <returns></returns>
        public async Task ModifyRole(int roleId, ManagerRoleModel req) 
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.SingleAsync(a => a.RoleId == roleId);
                data.ModifyDate = DateTime.Now;
                data.RoleName = req.roleName;
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 刪除角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public async Task DeleteRole(int roleId)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.SingleAsync(a => a.RoleId == roleId);
                db.ManagerRole.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 批次刪除角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task BatchDeleteManagerRole(List<int> req)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.ManagerRole.Where(a => req.Contains(a.RoleId)).ToListAsync();
                db.ManagerRole.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }
        
    }
    
}
