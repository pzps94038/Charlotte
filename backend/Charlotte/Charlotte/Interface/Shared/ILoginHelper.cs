using Charlotte.Model;
using Charlotte.Model.Shared;

namespace Charlotte.Interface.Shared
{
    public interface ILoginHelper<T>
    {
        Task<(string, T)> Login(LoginModel request);
    }
}
