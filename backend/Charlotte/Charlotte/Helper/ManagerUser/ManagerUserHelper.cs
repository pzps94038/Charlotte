using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Dapper;
using Mapster;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.ManagerUser
{
    public static class ManagerUserHelper
    {
        public static async Task CreateManagerUser(CreateManagerUserModel req) 
        {
            using (var db = new CharlotteContext()) 
            {
                var user = new ManagerMain();
                if(req.address != null) user.Address = req.address;
                user.Account = req.account;
                user.Email = req.email;
                user.RoleId = req.roleId;
                user.Flag = "Y";
                user.CreatedDate = DateTime.Now;
                user.Password = SHA256Helper.SHA256Encrypt(req.password);
                db.ManagerMain.Add(user);
                await db.SaveChangesAsync();
            }
        }
        public static async Task<ManagerUserVModel> GetManagerUser(int managerUserId) 
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            using (SqlConnection con = new SqlConnection(sqlConStr)) 
            {
                await con.OpenAsync();
                string sqlStr = @"Select UserName as userName, Email as email, Address as address, Birthday as birthday
                                  From ManagerMain
                                  Where ManagerUserId = @ManagerUserId";
                return await con.QueryFirstAsync<ManagerUserVModel>(sqlStr, new { ManagerUserId = managerUserId });
            }
        }
        public static async Task ModifyManagerUser(int managerUserId, ModifyManagerUserModel req) 
        {
            using (var db = new CharlotteContext())
            {
                var managerMain = db.ManagerMain.FirstOrDefault(a => a.ManagerUserId == managerUserId);
                if (managerMain == null) throw new NotFoundException() { };
                else 
                {
                    managerMain.UserName = req.userName;
                    managerMain.Email = req.email;
                    if(req.address != null) managerMain.Address = req.address;
                    managerMain.Birthday = req.birthday;
                }
                await db.SaveChangesAsync();
            }
        }
    }
}
