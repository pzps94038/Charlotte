using Charlotte.Enum;
using Charlotte.Helper.Login;
using Charlotte.Model;
using Charlotte.Model.Login;
using Charlotte.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginHelper _loginHelper;
        private readonly GetAppSettingsUtils _u;
        public LoginController(ILoginHelper helper, GetAppSettingsUtils uu)
        {
            _u = uu;
            _loginHelper = helper;
        }

        /// <summary>
        /// 購物平台登入
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<Token>> Login(LoginModel req) 
        {
            ResultModel<Token> result = new ResultModel<Token>();
            try
            {
                (string message , Token token) =  await _loginHelper.Login(req);
                if (string.IsNullOrEmpty(message))
                {
                    result.code = HttpStatusCode.OK;
                    result.message = EnumUtils.GetDescription(EnumResult.Success);
                    result.data = token;
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

        [HttpGet]
        public void test() 
        {
            var tx = _u.GetAppSetting("AES:Key");
            var a = "1";
            var b = "2";
        }
    }
}
