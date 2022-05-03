using Charlotte.Model.Register;

namespace Charlotte.Helper.Register
{
    public interface IRegisterHelper
    {
        Task<string> Register(RegisterModel req);
    }
}
