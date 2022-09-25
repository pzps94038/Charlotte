using System;
using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Interface.ProductType;
using Charlotte.VModel.ProductType;
using Microsoft.EntityFrameworkCore;

namespace Charlotte.Helper.ProductType
{
    public class ProductTypeHelper : IProductTypeHelper
    {
        public async Task<List<ProductTypeVModel>> GetAllAsync()
        {
            using (var db = new CharlotteContext())
            {
                var result = await db.ProductType.Select(a =>
                 new ProductTypeVModel()
                 {
                     ProductTypeId = a.ProductTypeId,
                     IconName = a.IconName,
                     IconType = a.IconType,
                     Type = a.Type
                 }).ToListAsync();

                return result;
            }
        }
    }
}

