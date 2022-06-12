using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Interface.ManagerProduct;
using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerProduct;
using Charlotte.VModel.ManagerProduct;
using Charlotte.VModel.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Charlotte.Helper.ManagerProduct
{
    public class ManagerProductHelper : IManagerProductHelper
    {
        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.ProductInformation.Where(a => idList.Contains(a.ProductId)).ToListAsync();
                db.ProductInformation.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(ManagerPorductModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = request.Adapt<ProductInformation>();
                data.CreateDate = DateTime.Now;
                db.ProductInformation.Add(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = db.ProductInformation.Single(a => a.ProductId == id);
                db.ProductInformation.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<string> FileUpload(List<IFormFile> files)
        {
            IFormFile file = files.First();
            string dicPath = "./FileUpload/Product";
            if (!Directory.Exists(dicPath))
                Directory.CreateDirectory(dicPath);
            string filePath = dicPath + "/" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss")+ "_" + file.FileName;
            using (var stream = File.Create(filePath))
                await file.CopyToAsync(stream);
            return filePath;
        }

        public async Task<TableVModel<ManagerProductVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.ProductInformation.Join(db.ProductType, a=> a.ProductTypeId, b=> b.ProductTypeId, (a,b)=> new 
                    // 產品類型
                    {
                        a.ProductId,
                        a.ProductName,
                        b.Type,
                        a.Inventory,
                        a.CostPrice,
                        a.SellPrice,
                        a.FactoryId,
                        a.ProductImgPath
                    } 
                ).Join(db.Factory, a=> a.FactoryId, b=> b.FactoryId, (a, b) => new ManagerProductVModel
                    // 廠商
                    {
                        ProductId = a.ProductId,
                        ProductName = a.ProductName,
                        Type = a.Type,
                        Inventory = a.Inventory,
                        CostPrice = a.CostPrice,
                        SellPrice = a.SellPrice,
                        FactoryName = b.FactoryName,
                        ProductImgPath = a.ProductImgPath
                }
                ).AsQueryable();
                if (filterStr != null)
                {
                    query = query.Where(a => 
                                            a.ProductId.ToString().Contains(filterStr) ||
                                            a.ProductName.Contains(filterStr) ||
                                            a.Type.Contains(filterStr) ||
                                            a.Inventory.ToString().Contains(filterStr) ||
                                            a.CostPrice.ToString().Contains(filterStr) ||
                                            a.SellPrice.ToString().Contains(filterStr)
                                       );
                }
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.ToListAsync();
                return new TableVModel<ManagerProductVModel>(result, tableTotalCount);
            }
        }

        public async Task ModifyAsync(int id, ManagerPorductModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = db.ProductInformation.Single(a => a.ProductId == id);
                data.ProductName = request.ProductName;
                data.ProductTypeId = request.ProductTypeId;
                data.Inventory = request.Inventory;
                data.CostPrice = request.CostPrice;
                data.SellPrice = request.SellPrice;
                data.FactoryId = request.FactoryId;
                data.ProductImgPath = request.ProductImgPath;
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
    }
}
