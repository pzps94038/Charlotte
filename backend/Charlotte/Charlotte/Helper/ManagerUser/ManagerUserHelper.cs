using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Enum;
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
        public async Task CreateManagerUser(CreateManagerUserModel req) 
        {
            using (var db = new CharlotteContext()) 
            {
                var user = new ManagerMain();
                if(req.address != null) user.Address = req.address;
                user.UserName = req.userName;
                user.Account = req.account;
                user.Email = req.email;
                user.RoleId = req.roleId;
                user.Flag = req.flag ? "Y" : "N";
                user.CreatedDate = DateTime.Now;
                user.Birthday = req.birthday;
                user.Password = EncryptUtils.SHA256Encrypt(req.password);
                db.ManagerMain.Add(user);
                await db.SaveChangesAsync();
            }
        }
        public async Task<ManagerUserVModel> GetManagerUser(int managerUserId) 
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr)) 
            {
                await con.OpenAsync();
                string sqlStr = @"Select UserName as userName, Email as email, Address as address, Birthday as birthday
                                  From ManagerMain
                                  Where ManagerUserId = @ManagerUserId";
                return await con.QueryFirstAsync<ManagerUserVModel>(sqlStr, new { ManagerUserId = managerUserId });
            }
        }
        public async Task<List<ManagerUsersVModel>> GetManagerUsers()
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                string sqlStr = @"SELECT main.ManagerUserId as managerUserId, main.UserName as userName, main.Account as account, main.Password as password,
                                main.Email as email, main.Address as address, main.Birthday as birthday, main.Flag as flag, main.RoleId as roleId,role.RoleName as roleName
                                FROM [Charlotte].[dbo].[ManagerMain] as main
                                left join ManagerRole as role on role.RoleId = main.RoleId";
                var data =  await con.QueryAsync<ManagerUsersVModel>(sqlStr);
                return data.ToList();
            }
        }
        public async Task ModifyManagerUser(int managerUserId, ModifyManagerUserModel req) 
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == managerUserId);
                if (req.userName != null) 
                    data.UserName = req.userName;
                if (req.account != null) 
                    data.Account = req.account;
                if (req.password != null) 
                    data.Password = EncryptUtils.SHA256Encrypt(req.password);
                if (req.email != null) 
                    data.Email = req.email;
                if (req.address != null) 
                    data.Address = req.address;
                if (req.birthday != null) 
                    data.Birthday = (DateTime)req.birthday;
                if (req.roleId != null) 
                    data.RoleId = (int)req.roleId;
                if (req.flag != null) 
                    data.Flag = (bool)req.flag ? "Y" : "N";
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteManagerUser(int managerUserId)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.SingleAsync(a=> a.ManagerUserId == managerUserId);
                db.ManagerMain.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteManagerUser(List<int> req)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.ManagerMain.Where(a=> req.Contains(a.ManagerUserId)).ToListAsync();
                db.ManagerMain.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

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
    }
}
