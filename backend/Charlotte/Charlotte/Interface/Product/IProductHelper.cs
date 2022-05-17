using Charlotte.Interface.Shared;
using Charlotte.VModel.Product;

namespace Charlotte.Helper.Product
{
    public interface IProductHelper: IGetAsync<ProductVModel>
    {
        Task<List<ProductVModel>> GetProducts(int? typeId);
    }
}
