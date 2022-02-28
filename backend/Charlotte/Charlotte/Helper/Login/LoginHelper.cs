﻿using Charlotte.Model;
using Charlotte.Model.DataBase;
using Charlotte.Model.Login;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Charlotte.Helper.Login
{
    public static class LoginHelper
    {
        private static string sqlConStr = GetAppSettingsHelper.GetConnectionString("Charlotte");
        public static (string , Token) Login(LoginModel req) 
        {   
            string message = "";
            Token token = new Token();
            using (SqlConnection con = new SqlConnection(sqlConStr))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        UserMain userMain = GetUserMain(con, transaction, req.account);
                        if (userMain == null) message = "帳號或密碼錯誤";
                        else
                        {
                            bool flag = CreateLoginLog(con, transaction, userMain, req.password); // 寫入登入Log
                            if (flag)
                            {
                                string refreshToken = JwtHelper.CreateRefreshToken();
                                CreateRefreshTokenLog(con, transaction, userMain, refreshToken);
                                var claims = JwtHelper.CreateClaims(userMain.Email, userMain.UserId.ToString());
                                string accountToken = JwtHelper.GenerateToken(claims);
                                token.AccessToken = accountToken;
                                token.RefreshToken = refreshToken;
                            }
                            else message = "帳號或密碼錯誤";
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex) 
                    {
                        transaction.Rollback();
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
        private static UserMain GetUserMain(SqlConnection con, SqlTransaction transaction, string account) 
        {
            string sqlStr = @"Select * From UserMain Where Account = @account ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("account", account);
            return con.QueryFirstOrDefault<UserMain>(sqlStr, parameters, transaction);
        }

        /// <summary>
        /// 寫入登入狀態Log
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="password">密碼</param>
        /// <returns>是否成功登入</returns>
        private static bool CreateLoginLog(SqlConnection con, SqlTransaction transaction, UserMain userMain, string password) 
        {
            string shaPwd = SHA256Helper.SHA256Encrypt(password);
            bool flag = userMain.Password == shaPwd;
            string sqlStr = @"INSERT INTO LoginLog
                            (UserId, LoginTime, Flag)
                            VALUES(@UserId, @LoginTime, @Flag)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserID", userMain.UserId);
            parameters.Add("LoginTime", DateTime.Now);
            parameters.Add("Flag", flag ? "Y" : "N");
            con.Execute(sqlStr, parameters, transaction);
            return flag;
        }

        /// <summary>
        /// 寫入RefreshToken紀錄
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="refreshToken">refreshToken</param>
        private static void CreateRefreshTokenLog(SqlConnection con, SqlTransaction transaction, UserMain userMain, string refreshToken) 
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