using fluffyspoon.registration.contracts.Grains;
using fluffyspoon.registration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Net;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HelloWorldController : ControllerBase
    {
        private readonly IClusterClient _client;

        public HelloWorldController(IClusterClient client)
        {
            _client = client;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public async Task<ActionResult> Post([FromBody] RegisterUserModel model)
        {
            await _client.GetGrain<IRegistrationGrain>(Guid.NewGuid()).RegisterAsync(model.Name, model.Surname, model.Email);

            return Accepted();
        }
    }
}