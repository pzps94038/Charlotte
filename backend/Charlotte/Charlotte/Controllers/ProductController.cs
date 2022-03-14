﻿using Charlotte.EnumMessage;
using Charlotte.Helper.Product;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// 取得單個產品資料
        /// </summary>
        /// <param name="productId">產品ID</param>
        /// <returns>該ID產品資料</returns>
        [HttpGet("{productId}")]
        
        public ResultModel<ProductVModel> GetProduct(int productId) 
        {
            ResultModel<ProductVModel> result = new ResultModel<ProductVModel>();            
            try
            {
                var data = ProductHelper.GetProruct(productId);
                if (data == null)
                {
                    result.message = EnumHelper.GetDescription(EnumResult.NotFound);
                    result.code = HttpStatusCode.NotFound;
                }
                else 
                {
                    result.message = EnumHelper.GetDescription(EnumResult.Success);
                    result.code = HttpStatusCode.OK;
                    result.data = data;
                }
                
            }
            catch (Exception ex) 
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.Fail);
                LoggerHelper.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 產品類型ID
        /// </summary>
        /// <param name="typeId">產品類型ID</param>
        /// <returns>符合該產品類型的產品，或是全部產品</returns>
        [HttpGet]
        public ResultModel<List<ProductVModel>> GetProducts(int? typeId)
        {
            ResultModel<List<ProductVModel>> result = new ResultModel<List<ProductVModel>>();
            try
            {
                result.data = ProductHelper.GetProducts(typeId);
                result.message = EnumHelper.GetDescription(EnumResult.Success);
                result.code= HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.Fail);
                LoggerHelper.Error(ex);
            }
            return result;
        }

    }
}
