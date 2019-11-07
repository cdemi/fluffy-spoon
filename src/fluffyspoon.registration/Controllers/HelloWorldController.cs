using Microsoft.AspNetCore.Mvc;

namespace fluffyspoon.registration.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("Hello Test!");
    }
}