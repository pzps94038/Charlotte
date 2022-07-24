using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerOrderDetail;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.ManagerOrderDetail;

namespace Charlotte.Interface.ManagerOrderDetail
{
    public interface IManagerOrderDetailHelper: IGetAsync<List<ManagerOrderDetailVModel>>, IModifyAsync<List<ManagerOrderDetailModel>>
    {
        public Task DeleteAsync(int orderId, int productId);
    }
}
