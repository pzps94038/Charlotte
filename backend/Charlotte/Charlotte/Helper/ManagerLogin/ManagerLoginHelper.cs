using Charlotte.DataBase.Model;
using Charlotte.Enum;
using Charlotte.Model;
using Charlotte.Model.ManagerLogin;
using Charlotte.Services;
using Charlotte.VModel.ManagerLogin;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;

namespace Charlotte.Helper.ManagerLogin
{
    public class ManagerLoginHelper: IManagerLoginHelper
    {
        public async Task<(string, ManagerLoginVModel)> Login(ManagerLoginModel req) 
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            string message = "";
            ManagerLoginVModel result = new ManagerLoginVModel();
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                using (var transaction = await con.BeginTransactionAsync())
                {
                    try
                    {
                        ManagerMain managerMain = await GetManagerMain(con, transaction, req.account);
                        if (managerMain == null) message = "帳號或密碼錯誤";
                        else if(managerMain.Flag != "Y") message = "帳號已鎖住，請洽管理員";
                        else
                        {
                            bool flag = CreateLoginLog(con, transaction, managerMain, req.password); // 寫入登入Log
                            if (flag)
                            {
                                string refreshToken = JwtHelper.CreateRefreshToken();
                                var claims = JwtHelper.CreateClaims(managerMain.Email, managerMain.ManagerUserId.ToString());
                                string accountToken = JwtHelper.GenerateToken(claims);
                                result.token.accessToken = accountToken;
                                result.token.refreshToken = refreshToken;
                                result.managerUserId = managerMain.ManagerUserId;
                                CreateRefreshTokenLog(con, transaction, managerMain, refreshToken);
                            }
                            else message = "帳號或密碼錯誤";
                        }
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return (message, result);
        }
        /// <summary>
        /// 用帳號取得使用者資訊
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="account">帳號</param>
        /// <returns>使用者資訊</returns>
        private async Task<ManagerMain> GetManagerMain(SqlConnection con, DbTransaction transaction, string account)
        {
            string sqlStr = @"Select * From ManagerMain Where Account = @account ";
            return await con.QueryFirstOrDefaultAsync<ManagerMain>(sqlStr, new { account = account }, transaction);
        }

        /// <summary>
        /// 寫入登入狀態Log
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="password">密碼</param>
        /// <returns>是否成功登入</returns>
        private bool CreateLoginLog(SqlConnection con, DbTransaction transaction, ManagerMain managerMain, string password)
        {
            string shaPwd = EncryptUtils.SHA256Encrypt(password);
            bool flag = managerMain.Password == shaPwd;
            string sqlStr = @"INSERT INTO ManagerLoginLog
                            (ManagerUserId, LoginTime, Flag)
                            VALUES(@ManagerUserId, @LoginTime, @Flag)";
            con.Execute(sqlStr, new { ManagerUserId = managerMain.ManagerUserId, LoginTime = DateTime.Now, Flag = flag ? "Y" : "N" }, transaction);
            return flag;
        }

        /// <summary>
        /// 寫入RefreshToken紀錄
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="refreshToken">refreshToken</param>
        private void CreateRefreshTokenLog(SqlConnection con, DbTransaction transaction, ManagerMain managerMain, string refreshToken)
        {
            string refreshToenExp = GetAppSettingsUtils.GetAppSettingsValue("JWT", "RefreshToenExpirationDate");
            string sqlStr = @"INSERT INTO ManagerRefreshTokenLog
                            (ManagerUserId, RefreshToken, CreateDate, ExpirationDate)
                            VALUES(@ManagerUserId ,@RefreshToken, @CreateDate, @ExpirationDate)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ManagerUserId", managerMain.ManagerUserId);
            parameters.Add("RefreshToken", refreshToken);
            parameters.Add("CreateDate", DateTime.Now);
            parameters.Add("ExpirationDate", DateTime.Now.AddDays(double.Parse(refreshToenExp)));
            con.Execute(sqlStr, parameters, transaction);
        }
    }
}
