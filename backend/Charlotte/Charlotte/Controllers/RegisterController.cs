using Charlotte.Enum;
using Charlotte.Helper;
using Charlotte.Interface.Register;
using Charlotte.Model;
using Charlotte.Model.Register;
using Charlotte.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterHelper _registerHelper;
        private readonly IHubContext<DashbordHub> _hubContext;
        public RegisterController(IRegisterHelper helper, IHubContext<DashbordHub> hubContext)
        {
            _registerHelper = helper;
            _hubContext = hubContext;
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
                    await _hubContext.Clients.All.SendAsync("RegisteredMemberRefresh");
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
