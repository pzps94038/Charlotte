using System;
using Charlotte.VModel.ProductType;

namespace Charlotte.Interface.ProductType
{
    public interface IProductTypeHelper
    {
        Task<List<ProductTypeVModel>> GetAllAsync();
    }
}

