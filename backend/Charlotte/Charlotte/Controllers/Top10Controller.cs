using Charlotte.Enum;
using Charlotte.Interface.Top10;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.Top10;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Top10Controller : ControllerBase
    {
        private readonly ITop10Helper _top10Helper;
        public Top10Controller(ITop10Helper top10Helper)
        {
            _top10Helper = top10Helper;
        }

        [HttpGet]
        public async Task<ResultModel<List<Top10Model>>> GetTop10()
        {
            ResultModel<List<Top10Model>> result = new ResultModel<List<Top10Model>>();
            try
            {
                result.Data = await _top10Helper.GetAsync();
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
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
