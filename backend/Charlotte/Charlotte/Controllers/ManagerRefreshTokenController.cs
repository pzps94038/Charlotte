using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerRefreshToken;
using Charlotte.Model;
using Charlotte.Model.ManagerRefreshToken;
using Charlotte.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRefreshTokenController : ControllerBase
    {
        [HttpPost]
        public async Task<ResultModel<Token>> RefreshToken(RefreshToken req)
        {
            var result = new ResultModel<Token>();
            try
            {
                result.data = await ManagerRefreshTokenHelper.RefreshToken(req);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.CreateSuccess);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.CreateFail);
                LoggerHelper.Error(ex);
            }
            return result;
        }
    }
}
