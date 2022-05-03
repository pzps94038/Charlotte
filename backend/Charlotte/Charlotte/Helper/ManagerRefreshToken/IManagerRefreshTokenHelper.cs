using Charlotte.Model;
using Charlotte.Model.ManagerRefreshToken;

namespace Charlotte.Helper.ManagerRefreshToken
{
    public interface IManagerRefreshTokenHelper
    {
        Task<Token> RefreshToken(RefreshToken req);
    }
}
