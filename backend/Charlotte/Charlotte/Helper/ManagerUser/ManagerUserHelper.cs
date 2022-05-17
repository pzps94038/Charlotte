using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Dapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

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
                if (userData.Password != EncryptUtils.SHA256Encrypt(req.password))
                    return result = "原密碼輸入錯誤";
                else 
                {
                    userData.Password = EncryptUtils.SHA256Encrypt(req.newPassword);
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
                if (request.address != null) 
                    user.Address = request.address;
                user.UserName = request.userName;
                user.Account = request.account;
                user.Email = request.email;
                user.RoleId = request.roleId;
                user.Flag = request.flag ? "Y" : "N";
                user.CreatedDate = DateTime.Now;
                user.Birthday = request.birthday;
                user.Password = EncryptUtils.SHA256Encrypt(request.password);
                db.ManagerMain.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ModifyManagerUserModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == id);
                if (request.userName != null)
                    data.UserName = request.userName;
                if (request.account != null)
                    data.Account = request.account;
                if (request.password != null)
                    data.Password = EncryptUtils.SHA256Encrypt(request.password);
                if (request.email != null)
                    data.Email = request.email;
                if (request.address != null)
                    data.Address = request.address;
                if (request.birthday != null)
                    data.Birthday = (DateTime)request.birthday;
                if (request.roleId != null)
                    data.RoleId = (int)request.roleId;
                if (request.flag != null)
                    data.Flag = (bool)request.flag ? "Y" : "N";
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

        public async Task<List<ManagerUsersVModel>> GetAllAsync()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT main.ManagerUserId, main.UserName, main.Account, main.Password,
                                main.Email, main.Address, main.Birthday, main.Flag, main.RoleId,role.RoleName
                                FROM [Charlotte].[dbo].[ManagerMain] as main
                                left join ManagerRole as role on role.RoleId = main.RoleId";
                var data = await con.QueryAsync<ManagerUsersVModel>(sqlStr);
                return data.ToList();
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
    }
}
