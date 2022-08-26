using Charlotte.Interface.Shared;
using Charlotte.Model.ProductType;
using Charlotte.VModel.ManagerProductType;
using Charlotte.VModel.Shared;

namespace Charlotte.Interface.ProductType
{
    public interface IProductTypeHelper: IGetAllAsync<TableVModel<MnaagerProductTypeVModel>>, IDeleteAsync, IBatchDeleteAsync, IModifyAsync<ManagerProductTypeModel>, ICreateAsync<ManagerProductTypeModel>
    {
    }
}
