using Charlotte.Enum;
using Charlotte.Helper.Login;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.Shared;
using Charlotte.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Charlotte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly ILoginHelper<Token> _loginHelper;
        public LoginController(ILoginHelper<Token> helper)
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
                    result.Code = HttpStatusCode.OK;
                    result.Message = EnumUtils.GetDescription(EnumResult.Success);
                    result.Data = token;
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
