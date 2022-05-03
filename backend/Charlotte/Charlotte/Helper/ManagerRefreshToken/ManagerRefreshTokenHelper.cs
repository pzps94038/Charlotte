using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Model;
using Charlotte.Model.ManagerRefreshToken;
using Charlotte.Services;
using Microsoft.EntityFrameworkCore;
using static Charlotte.CustomizeException.CustomizeException;

namespace Charlotte.Helper.ManagerRefreshToken
{
    public static class ManagerRefreshTokenHelper
    {
        public static async Task<Token> RefreshToken(RefreshToken req) 
        {
            using (var db = new CharlotteContext())
            {
                var log = await db.ManagerRefreshTokenLog.SingleAsync(a=> a.RefreshToken == req.refreshToken && a.ManagerUserId == req.userId);
                if (log.ExpirationDate < DateTime.Now)
                    throw new TokenExpiredException();
                else 
                {
                    var userMain = await db.ManagerMain.SingleAsync(a => a.ManagerUserId == req.userId);
                    string refreshToken = JwtHelper.CreateRefreshToken();
                    var claims = JwtHelper.CreateClaims(userMain.Email, userMain.ManagerUserId.ToString());
                    string accountToken = JwtHelper.GenerateToken(claims);
                    CreateRefreshTokenLog(db, req.userId, refreshToken);
                    await db.SaveChangesAsync();
                    return new Token(accountToken, refreshToken);
                }
            }
        }

        /// <summary>
        /// 寫入RefreshToken紀錄
        /// </summary>
        /// <param name="con">資料庫連接</param>
        /// <param name="transaction">交易範圍</param>
        /// <param name="userMain">使用者資訊</param>
        /// <param name="refreshToken">refreshToken</param>
        private static void CreateRefreshTokenLog(CharlotteContext db, int userId, string refreshToken)
        {
            string refreshToenExp = GetAppSettingsHelper.GetAppSettingsValue("JWT", "RefreshToenExpirationDate");
            var data = new ManagerRefreshTokenLog();
            data.RefreshToken = refreshToken;
            data.CreateDate = DateTime.Now;
            data.ExpirationDate = DateTime.Now.AddDays(double.Parse(refreshToenExp));
            data.ManagerUserId = userId;
            db.ManagerRefreshTokenLog.Add(data);
        }
    }
}
