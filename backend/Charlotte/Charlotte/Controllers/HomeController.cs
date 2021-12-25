using Charlotte.Helper;
using Charlotte.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public void Login() 
        {
            
        }
        [HttpPost]
        [Route("Register")]
        public void Register()
        {

        }

        [HttpPost]
        [Route("Logout")]
        public void Logout()
        {

        }
    }
}
