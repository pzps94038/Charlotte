using Charlotte.DataBase.DbContextModel;
using Charlotte.Model.ManagerRole;
using Charlotte.Database.Model;
using System.Data.SqlClient;
using Charlotte.Services;
using Charlotte.VModel.ManagerRole;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.ManagerRole
{
    public static class ManagerRoleHelper
    {
        public static async Task CreateManagerRole(ManagerRoleModel req) 
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

        public static async Task<List<ManagerRoleVModel>> GetRoles()
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT RoleId as roleId, RoleName as roleName,  convert(varchar, CreateDate, 23) as createDate, convert(varchar, ModifyDate, 23) as modifyDate
                                FROM ManagerRole";
                var result = await con.QueryAsync<ManagerRoleVModel>(sqlStr);
                return result.ToList();
            }
        }

        public static async Task ModifyRole(int roleId, ManagerRoleModel req) 
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.FirstOrDefaultAsync(a => a.RoleId == roleId);
                if (data == null) throw new NotFoundException();
                else 
                {
                    data.ModifyDate = DateTime.Now;
                    data.RoleName = req.roleName;
                }
                await db.SaveChangesAsync();
            }
        }

        public static async Task DeleteRole(int roleId)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.FirstOrDefaultAsync(a => a.RoleId == roleId);
                if (data == null) throw new NotFoundException();
                else
                    db.ManagerRole.Remove(data);
                await db.SaveChangesAsync();
            }
        }
        public static async Task BatchDeleteManagerRole(List<int> req)
        {
            using (var db = new CharlotteContext())
            {
                foreach (var roleId in req) 
                {
                    var data = await db.ManagerRole.FirstOrDefaultAsync(a => a.RoleId == roleId);
                    if (data == null) throw new NotFoundException();
                    else
                        db.ManagerRole.Remove(data);
                }
                await db.SaveChangesAsync();
            }
        }
        
    }
    
}
