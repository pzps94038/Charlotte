using Charlotte.Enum;
using Charlotte.Helper.ManagerLogin;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.Shared;
using Charlotte.Services;
using Charlotte.VModel.ManagerLogin;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILoginHelper<ManagerLoginVModel> _loginHelper;
        public ManagerLoginController(ILoginHelper<ManagerLoginVModel> helper)
        {
            _loginHelper = helper;
        }

        /// <summary>
        /// 管理平台登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<ManagerLoginVModel>> Login(LoginModel req)
        {

            var result = new ResultModel<ManagerLoginVModel>();
            try
            {
                (string message, ManagerLoginVModel data) = await _loginHelper.Login(req);
                if (string.IsNullOrEmpty(message))
                {
                    result.Code = HttpStatusCode.OK;
                    result.Message = EnumUtils.GetDescription(EnumResult.Success);
                    result.Data = data;
                }
                else
                {
                    result.Code = HttpStatusCode.BadRequest;
                    result.Message = message;
                }
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
