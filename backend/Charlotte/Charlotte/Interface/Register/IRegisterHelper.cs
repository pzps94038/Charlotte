using Charlotte.Model.Register;

namespace Charlotte.Interface.Register
{
    public interface IRegisterHelper
    {
        Task<string> Register(RegisterModel req);
    }
}
