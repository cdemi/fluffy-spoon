using System;
using System.Net;
using System.Threading.Tasks;
using DemoFluffySpoon.Contracts.Grains;
using DemoFluffySpoon.Contracts.Models;
using DemoFluffySpoon.Registration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace DemoFluffySpoon.Registration.Controllers
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
        public async Task<ActionResult<RegistrationStatusState>> Get([FromRoute] Guid id)
        {
            var result = await _client.GetGrain<IUserRegistrationStatusGrain>(id).GetAsync();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        public async Task<ActionResult<Guid>> Post([FromBody] RegisterUserModel model)
        {
            var id = await _client
                .GetGrain<IUserRegistrationGrain>(model.Email.ToLower())
                .RegisterAsync(model.Name, model.Surname);

            if (id.HasValue)
            {
                return Accepted(id);
            }

            return Conflict();
        }
    }
}