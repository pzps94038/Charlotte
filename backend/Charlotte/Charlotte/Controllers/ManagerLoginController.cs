using Charlotte.Enum;
using Charlotte.Helper.ManagerLogin;
using Charlotte.Model;
using Charlotte.Model.ManagerLogin;
using Charlotte.Model.Shared;
using Charlotte.Services;
using Charlotte.VModel.ManagerLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerLoginController : ControllerBase
    {
        private readonly IManagerLoginHelper _loginHelper;
        public ManagerLoginController(IManagerLoginHelper helper)
        {
            _loginHelper = helper;
        }

        /// <summary>
        /// 管理平台登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<ManagerLoginVModel>> Login(ManagerLoginModel req)
        {

            var result = new ResultModel<ManagerLoginVModel>();
            try
            {
                (string message, ManagerLoginVModel data) = await _loginHelper.Login(req);
                if (string.IsNullOrEmpty(message))
                {
                    result.code = HttpStatusCode.OK;
                    result.message = EnumUtils.GetDescription(EnumResult.Success);
                    result.data = data;
                }
                else
                {
                    result.code = HttpStatusCode.BadRequest;
                    result.message = message;
                }
            }
            catch (Exception ex) 
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
