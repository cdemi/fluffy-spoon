using demofluffyspoon.contracts.Grains;
using demofluffyspoon.contracts.Models;
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistrationState>> Get([FromRoute] Guid id)
        {
            var result = await _client.GetGrain<IUserRegistrationGrain>(id).GetAsync();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public async Task<ActionResult<Guid>> Post([FromBody] RegisterUserModel model)
        {
            var id = Guid.NewGuid();

            await _client.GetGrain<IUserRegistrationGrain>(id).RegisterAsync(model.Name, model.Surname, model.Email);

            return Accepted(id);
        }
    }
}