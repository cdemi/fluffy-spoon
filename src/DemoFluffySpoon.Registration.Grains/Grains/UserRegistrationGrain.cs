using System;
using System.Threading.Tasks;
using DemoFluffySpoon.Contracts;
using DemoFluffySpoon.Contracts.Grains;
using DemoFluffySpoon.Contracts.Models;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;

namespace DemoFluffySpoon.Registration.Grains.Grains
{
    public class UserRegistrationGrain : Grain, IUserRegistrationGrain
    {
        private readonly IPersistentState<RegistrationState> _registrationState;
        private readonly ILogger<RegistrationState> _logger;

        private IAsyncStream<UserRegisteredEvent> _userRegisteredStream;

        public UserRegistrationGrain([PersistentState(nameof(RegistrationState))]
            IPersistentState<RegistrationState> registrationState, ILogger<RegistrationState> logger)
        {
            _registrationState = registrationState;
            _logger = logger;
        }

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);

            // Producer
            _userRegisteredStream =
                streamProvider.GetStream<UserRegisteredEvent>(_registrationState.State.Id, nameof(UserRegisteredEvent));

            await base.OnActivateAsync();
        }

        public async Task<Guid?> RegisterAsync(string name, string surname)
        {
            var email = this.GetPrimaryKeyString();

            if (_registrationState.State.IsRegistered)
            {
                _logger.LogWarning("{email} is already registered", email);

                return null;
            }

            await _userRegisteredStream.OnNextAsync(new UserRegisteredEvent
                {Name = name, Surname = surname, Email = email});
            _registrationState.State.IsRegistered = true;

            await _registrationState.WriteStateAsync();

            return _registrationState.State.Id;
        }
    }
}