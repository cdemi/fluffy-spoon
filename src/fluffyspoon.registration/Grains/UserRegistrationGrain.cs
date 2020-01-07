using demofluffyspoon.contracts;
using demofluffyspoon.contracts.Grains;
using demofluffyspoon.contracts.Models;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Grains
{
    public class UserRegistrationGrain : Grain<RegistrationState>, IUserRegistrationGrain
    {
        private readonly ILogger<RegistrationState> _logger;

        private IAsyncStream<UserRegisteredEvent> _userRegisteredStream;

        public UserRegistrationGrain(ILogger<RegistrationState> logger)
        {
            _logger = logger;
        }
        
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);

            // Producer
            _userRegisteredStream = streamProvider.GetStream<UserRegisteredEvent>(State.Id, nameof(UserRegisteredEvent));
            
            await base.OnActivateAsync();
        }
        
        public async Task<Guid?> RegisterAsync(string name, string surname)
        {
            var email = this.GetPrimaryKeyString();

            if (State.IsRegistered)
            {
                _logger.LogWarning("{email} is already registered", email);
                
                return null;
            }

            await _userRegisteredStream.OnNextAsync(new UserRegisteredEvent {Name = name, Surname = surname, Email = email});
            State.IsRegistered = true;

            return State.Id;
        }
    }
}