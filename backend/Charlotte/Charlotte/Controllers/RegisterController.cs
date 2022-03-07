using Charlotte.EnumMessage;
using Charlotte.Helper;
using Charlotte.Helper.Register;
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
        [HttpPost]
        public ResultModel Register(RegisterModel req) 
        {
            ResultModel result = new ResultModel();
            try
            {
                string message =  RegisterHelper.Register(req);
                if (string.IsNullOrEmpty(message))
                {
                    result.code = HttpStatusCode.OK;
                    result.message = EnumHelper.GetDescription(EnumResult.Success);
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
