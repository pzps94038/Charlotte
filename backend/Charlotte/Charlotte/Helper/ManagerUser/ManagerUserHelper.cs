using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;
using Dapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;
using System.Linq.Dynamic.Core;
namespace Charlotte.Helper.ManagerUser
{
    public class ManagerUserHelper: IManagerUserHelper
    {

        public async Task<string> ModifyManagerUserPassword(int managerUserId, ModifyManagerUserPassWordModel req) 
        {
            string result = "";
            using (var db = new CharlotteContext())
            {
                var userData = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == managerUserId);
                if (userData.Password != EncryptUtils.SHA256Encrypt(req.Password))
                    return result = "原密碼輸入錯誤";
                else 
                {
                    userData.Password = EncryptUtils.SHA256Encrypt(req.NewPassword);
                    userData.ModifyDate = DateTime.Now;
                    await db.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task CreateAsync(CreateManagerUserModel request)
        {
            using (var db = new CharlotteContext())
            {
                var user = new ManagerMain();
                if (request.Address != null) 
                    user.Address = request.Address;
                user.UserName = request.UserName;
                user.Account = request.Account;
                user.Email = request.Email;
                user.RoleId = request.RoleId;
                user.Flag = request.Flag ? "Y" : "N";
                user.CreatedDate = DateTime.Now;
                user.Birthday = Convert.ToDateTime(request.Birthday);
                user.Password = EncryptUtils.SHA256Encrypt(request.Password);
                db.ManagerMain.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ModifyManagerUserModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == id);
                if (request.UserName != null)
                    data.UserName = request.UserName;
                if (request.Account != null)
                    data.Account = request.Account;
                if (request.password != null && data.Password != request.password)
                    data.Password = EncryptUtils.SHA256Encrypt(request.password);
                if (request.Email != null)
                    data.Email = request.Email;
                if (request.Address != null)
                    data.Address = request.Address;
                if (request.Birthday != null)
                    data.Birthday = (DateTime)request.Birthday;
                if (request.RoleId != null)
                    data.RoleId = (int)request.RoleId;
                if (request.Flag != null)
                    data.Flag = (bool)request.Flag ? "Y" : "N";
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == id);
                db.ManagerMain.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.ManagerMain.Where(a => idList.Contains(a.ManagerUserId)).ToListAsync();
                db.ManagerMain.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task<ManagerUserVModel> GetAsync(int id)
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"Select UserName, Email, Address, Birthday
                                  From ManagerMain
                                  Where ManagerUserId = @id";
                return await con.QueryFirstAsync<ManagerUserVModel>(sqlStr, new { id });
            }
        }

        public async Task<TableVModel<ManagerUsersVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.ManagerMain.Join(db.ManagerRole, a => a.RoleId, b => b.RoleId, (a, b) => new
                {
                    ManagerUserId = a.ManagerUserId,
                    UserName = a.UserName,
                    Account = a.Account,
                    Password = a.Password,
                    Email = a.Email,
                    Address = a.Address,
                    Birthday = a.Birthday.ToString("yyyy-MM-dd"),
                    Flag = a.Flag,
                    RoleId = a.RoleId,
                    RoleName = b.RoleName
                });
                if (filterStr != null)
                {
                    query = query.Where(a =>  a.UserName.Contains(filterStr) ||                    
                                                            a.Email.Contains(filterStr) ||
                                                            a.Flag.Contains(filterStr) ||
                                                            a.RoleName.Contains(filterStr)
                                                      );
                };
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.ToListAsync();
                return new TableVModel<ManagerUsersVModel>(result.Adapt<List<ManagerUsersVModel>>(), tableTotalCount) ;
            }
        }
    }
}
