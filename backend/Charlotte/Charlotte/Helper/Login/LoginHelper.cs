using Charlotte.DataBase.Model;
using Charlotte.Model;
using Charlotte.Model.Login;
using Charlotte.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Charlotte.Helper.Login
{
    public class LoginHelper: ILoginHelper
    {
        
        public async Task<(string , Token)> Login(LoginModel req) 
        {
            string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
            string message = "";
            Token token = new Token();
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                await con.OpenAsync();
                using (var transaction = await con.BeginTransactionAsync())
                {
                    try
                    {
                        UserMain userMain = await GetUserMain(con, transaction, req.account);
                        if (userMain == null) message = "帳號或密碼錯誤";
                        else
                        {
                            bool flag = CreateLoginLog(con, transaction, userMain, req.password); // 寫入登入Log
                            if (flag)
                            {
                                string refreshToken = JwtHelper.CreateRefreshToken();
                                var claims = JwtHelper.CreateClaims(userMain.Email, userMain.UserId.ToString());
                                string accountToken = JwtHelper.GenerateToken(claims);
                                token.accessToken = accountToken;
                                token.refreshToken = refreshToken;
                                CreateRefreshTokenLog(con, transaction, userMain, refreshToken);
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
            return (message , token);
        }

        /// <summary>
        /// 用帳號取得使用者資訊
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="account">帳號</param>
        /// <returns>使用者資訊</returns>
        private async Task<UserMain> GetUserMain(SqlConnection con, DbTransaction transaction, string account) 
        {
            string sqlStr = @"Select * From UserMain Where Account = @account ";
            return await con.QueryFirstOrDefaultAsync<UserMain>(sqlStr, new { account = account}, transaction);
        }

        /// <summary>
        /// 寫入登入狀態Log
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="password">密碼</param>
        /// <returns>是否成功登入</returns>
        private bool CreateLoginLog(SqlConnection con, DbTransaction transaction, UserMain userMain, string password) 
        {
            string shaPwd = EncryptHelper.SHA256Encrypt(password);
            bool flag = userMain.Password == shaPwd;
            string sqlStr = @"INSERT INTO LoginLog
                            (UserId, LoginTime, Flag)
                            VALUES(@UserId, @LoginTime, @Flag)";
            con.Execute(sqlStr, new { UserID = userMain.UserId, LoginTime = DateTime.Now, Flag = flag ? "Y" : "N" }, transaction);
            return flag;
        }

        /// <summary>
        /// 寫入RefreshToken紀錄
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="refreshToken">refreshToken</param>
        private void CreateRefreshTokenLog(SqlConnection con, DbTransaction transaction, UserMain userMain, string refreshToken) 
        {
            string refreshToenExp = GetAppSettingsHelper.GetAppSettingsValue("JWT", "RefreshToenExpirationDate");
            string sqlStr = @"INSERT INTO RefreshTokenLog
                            (UserId, RefreshToken, CreateDate, ExpirationDate)
                            VALUES(@UserId ,@RefreshToken, @CreateDate, @ExpirationDate)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserID", userMain.UserId);
            parameters.Add("RefreshToken", refreshToken);
            parameters.Add("CreateDate", DateTime.Now);
            parameters.Add("ExpirationDate", DateTime.Now.AddDays(double.Parse(refreshToenExp)));
            con.Execute(sqlStr, parameters, transaction);
        }
    }
}
