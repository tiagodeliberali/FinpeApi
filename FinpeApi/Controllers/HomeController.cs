using Microsoft.AspNetCore.Mvc;

namespace FinpeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "hi!";
        }
    }
}
