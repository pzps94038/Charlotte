using Charlotte.Interface.Shared;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.Shared;

namespace Charlotte.Interface.ManagerOrder
{
    public interface IManagerOrderHelper: IGetAllAsync<TableVModel<ManagerOrderVModel>>, IDeleteAsync, IBatchDeleteAsync
    {
    }
}
