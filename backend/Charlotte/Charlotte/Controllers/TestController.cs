using Charlotte.Enum;
using Charlotte.Helper.Login;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.Shared;
using Charlotte.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Charlotte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TestController : ControllerBase
    {

        /// <summary>
        /// 測試連線用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel Test() 
        {
            ResultModel result = new ResultModel();
            try
            {
                LoggerUtils.Info("測試寫入");
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
