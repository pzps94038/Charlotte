using Charlotte.Enum;
using Charlotte.Interface.ManagerOrderDetail;
using Charlotte.Model;
using Charlotte.Model.ManagerOrderDetail;
using Charlotte.Services;
using Charlotte.VModel.ManagerOrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerOrderDetailController : ControllerBase
    {
        private readonly IManagerOrderDetailHelper _orderDetailHelper;
        public ManagerOrderDetailController(IManagerOrderDetailHelper helper)
        {
            _orderDetailHelper = helper;
        }
        [HttpGet("{orderId}")]
        public async Task<ResultModel<List<ManagerOrderDetailVModel>>> GetOrderDetail(int orderId) 
        {
            ResultModel<List<ManagerOrderDetailVModel>> result = new ResultModel<List<ManagerOrderDetailVModel>>();
            try
            {
                result.Data = await _orderDetailHelper.GetAsync(orderId);
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                result.Code = HttpStatusCode.BadRequest;
            }
            catch (Exception ex) 
            {
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpPatch("{orderId}")]
        public async Task<ResultModel> ModifyOrderDetail(int orderId, List<ManagerOrderDetailModel> request)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _orderDetailHelper.ModifyAsync(orderId, request);
                result.Message = EnumUtils.GetDescription(EnumResult.ModifySuccess);
                result.Code = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                result.Message = EnumUtils.GetDescription(EnumResult.ModifyFail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpDelete]
        public async Task<ResultModel> DeleteOrderDetail(int orderId, int productId)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _orderDetailHelper.DeleteAsync(orderId, productId);
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                result.Code = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
