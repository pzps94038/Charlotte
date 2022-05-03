using Charlotte.Model.ManagerRouter;
using Charlotte.VModel.ManagerRouter;

namespace Charlotte.Helper.ManagerRouter
{
    public interface IManagerRouterHelper
    {
        Task<List<ManagerRouterVModel>> GetRouters();
        Task CreateRouter(ManagerRouterModel rq);
        Task ModifyRouter(int routerId, ManagerRouterModel rq);
        Task DeleteRouter(int routerId);
        Task BatchDeleteRouter(List<int> rq);
    }
}
