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
using Charlotte.Interface.Shared;
using Charlotte.VModel.Shared;
using Mapster;
using System.Linq.Dynamic.Core;
using Charlotte.Interface.ManagerRole;

namespace Charlotte.Helper.ManagerRole
{
    public class ManagerRoleHelper: IManagerRoleHelper
    {

        public async Task<List<ManagerRoleVModel>> GetAllAsync()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT RoleId, RoleName,  convert(varchar, CreateDate, 23) as CreateDate, convert(varchar, ModifyDate, 23) as ModifyDate
                                FROM ManagerRole";
                var result = await con.QueryAsync<ManagerRoleVModel>(sqlStr);
                return result.ToList();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.SingleAsync(a => a.RoleId == id);
                db.ManagerRole.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ManagerRoleModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerRole.SingleAsync(a => a.RoleId == id);
                data.ModifyDate = DateTime.Now;
                data.RoleName = request.RoleName;
                await db.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(ManagerRoleModel request)
        {
            using (var db = new CharlotteContext())
            {
                var role = new Database.Model.ManagerRole();
                role.RoleName = request.RoleName;
                role.CreateDate = DateTime.Now;
                db.ManagerRole.Add(role);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.ManagerRole.Where(a => idList.Contains(a.RoleId)).ToListAsync();
                db.ManagerRole.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task<TableVModel<ManagerRoleVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.ManagerRole.AsQueryable();
                if (filterStr != null)
                {
                    query = query.Where(a => a.RoleId.ToString().Contains(filterStr) ||
                                                      a.RoleName.Contains(filterStr)
                                        );
                }
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.Select(a=> new 
                    {
                        RoleId = a.RoleId,
                        RoleName = a.RoleName,
                        CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                        ModifyDate = a.ModifyDate.HasValue ? a.ModifyDate.Value.ToString("yyyy-MM-dd") : null,
                    }
                ).ToListAsync();
                return new TableVModel<ManagerRoleVModel>(result.Adapt<List<ManagerRoleVModel>>(), tableTotalCount);
            }
        }
    }
    
}
