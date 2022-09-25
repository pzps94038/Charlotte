using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.ProductType;
using Charlotte.Interface.Shared;
using Charlotte.Model.ProductType;
using Charlotte.VModel.ManagerProductType;
using Charlotte.VModel.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Charlotte.Helper.ManagerProductType
{
    public class ManagerProductTypeHelper : IManagerProductTypeHelper
    {
        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var dataList = db.ProductType.Where(a=> idList.Contains(a.ProductTypeId)).ToList();
                db.ProductType.RemoveRange(dataList);
                await db.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(ManagerProductTypeModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = new DataBase.Model.ProductType();
                data.Type = request.Type;
                data.IconName = request.IconName;
                data.IconType = request.IconType;
                data.CreateDate = DateTime.Now;
                db.ProductType.Add(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = db.ProductType.Single(a => a.ProductTypeId == id);
                db.ProductType.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<TableVModel<MnaagerProductTypeVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.ProductType.AsQueryable();
                if (filterStr != null)
                {
                    query = query.Where(a => a.ProductTypeId.ToString().Contains(filterStr) ||
                                             a.Type.Contains(filterStr) ||
                                             a.IconName.Contains(filterStr) ||
                                             a.IconType.Contains(filterStr)
                                        );
                }
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.Select(a=> new 
                    { 
                        ProductTypeId = a.ProductTypeId,
                        Type = a.Type,
                        IconType = a.IconType,
                        IconName = a.IconName,
                        CreateDate = a.CreateDate.ToString("yyyy-MM-dd"), 
                        ModifyDate = a.ModifyDate.HasValue ? a.ModifyDate.Value.ToString("yyyy-MM-dd") : ""
                    }
                ).ToListAsync();
                return new TableVModel<MnaagerProductTypeVModel>(result.Adapt<List<MnaagerProductTypeVModel>>(), tableTotalCount);
            }
        }

        public async Task ModifyAsync(int id, ManagerProductTypeModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = db.ProductType.Single(a => a.ProductTypeId == id);
                data.Type = request.Type;
                data.IconName = request.IconName;
                data.IconType = request.IconType;
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
    }
}
