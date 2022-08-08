using Charlotte.Enum;
using Charlotte.Interface.ManagerOrder;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.ManagerOrder;
using Charlotte.Services;
using Charlotte.VModel.ManagerMenu;
using Charlotte.VModel.ManagerOrder;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ManagerUser")]
    public class ManagerOrderController : ControllerBase
    {
        private readonly IManagerOrderHelper _orderHelper;
        private readonly IHubContext<DashbordHub> _hubContext;
        public ManagerOrderController(IManagerOrderHelper helper, IHubContext<DashbordHub> hubContext)
        {
            _orderHelper = helper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerOrderVModel>>> GetOrders(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr) 
        {
            ResultModel<TableVModel<ManagerOrderVModel>> result = new ResultModel<TableVModel<ManagerOrderVModel>>();
            try
            {
                result.Data = await _orderHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
                result.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpPost]
        public async Task<ResultModel> CreateOrder(ManagerOrderModel request)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _orderHelper.CreateAsync(request);
                result.Message = EnumUtils.GetDescription(EnumResult.CreateSuccess);
                result.Code = HttpStatusCode.OK;
                await _hubContext.Clients.All.SendAsync("WeekSaleRefresh");
                await _hubContext.Clients.All.SendAsync("MonthSaleRefresh");
            }
            catch (Exception ex)
            {
                result.Message = EnumUtils.GetDescription(EnumResult.CreateFail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpPatch("{orderId}")]
        public async Task<ResultModel> ModifyProduct(int orderId, ManagerOrderModel request)
        {
            var result = new ResultModel();
            try
            {
                await _orderHelper.ModifyAsync(orderId, request);
                result.Message = EnumUtils.GetDescription(EnumResult.ModifySuccess);
                result.Code = HttpStatusCode.OK;
                await _hubContext.Clients.All.SendAsync("WeekSaleRefresh");
                await _hubContext.Clients.All.SendAsync("MonthSaleRefresh");
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.ModifyFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpDelete("{orderId}")]
        public async Task<ResultModel> DeleteOrder(int orderId) 
        {
            ResultModel result = new ResultModel();
            try
            {
                await _orderHelper.DeleteAsync(orderId);
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                result.Code = HttpStatusCode.OK;
                await _hubContext.Clients.All.SendAsync("WeekSaleRefresh");
                await _hubContext.Clients.All.SendAsync("MonthSaleRefresh");
            }
            catch(Exception ex) 
            {
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                result.Code = HttpStatusCode.BadRequest;
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpDelete]
        public async Task<ResultModel> BatchDeleteOrders(List<int> request)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _orderHelper.BatchDeleteAsync(request);
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                result.Code = HttpStatusCode.OK;
                await _hubContext.Clients.All.SendAsync("WeekSaleRefresh");
                await _hubContext.Clients.All.SendAsync("MonthSaleRefresh");
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
