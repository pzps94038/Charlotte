using Charlotte.Enum;
using Charlotte.Helper.ManagerMenu;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.ManagerMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerMenuController : ControllerBase
    {

        private readonly IManagerMenuHelper _menuHelper;
        public ManagerMenuController(IManagerMenuHelper helper)
        {
            _menuHelper = helper;
        }

        [HttpGet("{userId}")]
        public async Task<ResultModel<List<ManagerMenuVModel>>> GetMenu(int userId)
        {
            ResultModel<List<ManagerMenuVModel>> result = new ResultModel<List<ManagerMenuVModel>>();
            try
            {
                result.data = await _menuHelper.GetMenu(userId);
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.Success);

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
