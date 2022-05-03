using Charlotte.EnumMessage;
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
        public LoginController(ILoginHelper helper)
        {
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
                    result.message = EnumHelper.GetDescription(EnumResult.Success);
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
                result.message = EnumHelper.GetDescription(EnumResult.Fail);
                LoggerHelper.Error(ex);
            }
            return result;
        }
    }
}
