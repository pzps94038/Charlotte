using Charlotte.DataBase.DbContextModel;
using Charlotte.Enum;
using Charlotte.Interface.Dashbord;
using Charlotte.Interface.Register;
using Charlotte.Model;
using Charlotte.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashbordController : ControllerBase
    {
        private readonly IDashbordHelper _dashbordHelper;
        public DashbordController(IDashbordHelper helper)
        {
            _dashbordHelper = helper;
        }

        [HttpGet]
        [Route("WeekSale")]
        public async Task<ResultModel<object>> WeekSale()
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                result.Data = await _dashbordHelper.WeekSale();
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpGet]
        [Route("MonthSale")]
        public async Task<ResultModel<object>> MonthSale()
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                result.Data = await _dashbordHelper.MonthSale();
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpGet]
        [Route("RegisteredMember")]
        public async Task<ResultModel<object>> RegisteredMember()
        {
            ResultModel<object> result = new ResultModel<object>();
            try
            {
                result.Data = await _dashbordHelper.RegisteredMember();
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
