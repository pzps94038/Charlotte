﻿using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
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
    public static class ManagerUserHelper
    {
        public static async Task CreateManagerUser(CreateManagerUserModel req) 
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
        public static async Task<List<ManagerUsersVModel>> GetManagerUsers()
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
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
        public static async Task ModifyManagerUser(int managerUserId, ModifyManagerUserModel req) 
        {
            using (var db = new CharlotteContext())
            {
                var data = db.ManagerMain.FirstOrDefault(a => a.ManagerUserId == managerUserId);
                if (data == null) throw new NotFoundException();
                else 
                {
                    if(req.userName != null) data.UserName = req.userName;
                    if(req.account != null) data.Account = req.account;
                    if(req.password != null) data.Password = SHA256Helper.SHA256Encrypt(req.password);
                    if(req.email != null) data.Email = req.email;
                    if(req.address != null) data.Address = req.address;
                    if(req.birthday != null) data.Birthday = (DateTime)req.birthday;
                    if(req.roleId != null) data.RoleId = (int)req.roleId;
                    if(req.flag != null) data.Flag = (bool)req.flag ? "Y" : "N";
                    data.ModifyDate = DateTime.Now;
                }
                await db.SaveChangesAsync();
            }
        }

        public static async Task DeleteManagerUser(int managerUserId)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.ManagerMain.FirstOrDefaultAsync(a=> a.ManagerUserId == managerUserId);
                if (data == null) throw new NotFoundException();
                else 
                {
                    db.ManagerMain.Remove(data);
                    await db.SaveChangesAsync();
                }
            }
        }

        public static async Task BatchDeleteManagerUser(List<int> req)
        {
            using (var db = new CharlotteContext())
            {
                foreach (var managerUserId in req) 
                {
                    var data = await db.ManagerMain.FirstOrDefaultAsync(a => a.ManagerUserId == managerUserId);
                    if (data == null) throw new NotFoundException();
                    else
                        db.ManagerMain.Remove(data);
                }
                await db.SaveChangesAsync();
            }
        }
    }
}