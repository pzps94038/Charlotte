using Charlotte.Model;

namespace Charlotte.Interface.RefreshToken
{
    public interface IRefreshTokenHelper
    {
        Task<Token> RefreshToken(Model.ManagerRefreshToken.RefreshToken request);
    }
}
