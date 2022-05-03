using Charlotte.VModel.ManagerMenu;

namespace Charlotte.Helper.ManagerMenu
{
    public interface IManagerMenuHelper
    {
        Task<List<ManagerMenuVModel>> GetMenu(int userId);
    }
}
