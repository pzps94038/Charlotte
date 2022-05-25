using Charlotte.Enum;
using Charlotte.Helper;
using Charlotte.Interface.Register;
using Charlotte.Model;
using Charlotte.Model.Register;
using Charlotte.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterHelper _registerHelper;
        public RegisterController(IRegisterHelper helper)
        {
            _registerHelper = helper;
        }

        /// <summary>
        /// 購物平台註冊
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> Register(RegisterModel req) 
        {
            ResultModel result = new ResultModel();
            try
            {
                string message =  await _registerHelper.Register(req);
                if (string.IsNullOrEmpty(message))
                {
                    result.Code = HttpStatusCode.OK;
                    result.Message = EnumUtils.GetDescription(EnumResult.Success);
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
