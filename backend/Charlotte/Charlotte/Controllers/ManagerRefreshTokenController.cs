using Charlotte.Enum;
using Charlotte.Interface.RefreshToken;
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
        private readonly IRefreshTokenHelper _managerRefreshTokenHelper;
        public ManagerRefreshTokenController(IRefreshTokenHelper helper)
        {
            _managerRefreshTokenHelper = helper;
        }

        [HttpPost]
        public async Task<ResultModel<Token>> RefreshToken(RefreshTokenModel req)
        {
            var result = new ResultModel<Token>();
            try
            {
                result.Data = await _managerRefreshTokenHelper.RefreshToken(req);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.CreateSuccess);

            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.CreateFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
