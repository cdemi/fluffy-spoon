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
    [Route("v{version:apiVersion}/register")]
    [ApiVersion("1")]
    public class RegisterController : ControllerBase
    {
        private readonly IClusterClient _client;

        public RegisterController(IClusterClient client)
        {
            _client = client;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public async Task<ActionResult> Post([FromBody] RegisterUserModel model)
        {
            await _client.GetGrain<IUserRegistrationGrain>(Guid.NewGuid()).RegisterAsync(model.Name, model.Surname, model.Email);

            return Accepted();
        }
    }
}