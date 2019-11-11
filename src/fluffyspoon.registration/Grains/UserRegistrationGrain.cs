using demofluffyspoon.contracts;
using demofluffyspoon.contracts.Grains;
using demofluffyspoon.contracts.Models;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Grains
{
    public class UserRegistrationGrain : Grain<UserRegistrationState>, IUserRegistrationGrain, IAsyncObserver<UserVerifiedEvent>
    {
        private IAsyncStream<UserRegisteredEvent> _userRegisteredStream;

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);

            _userRegisteredStream = streamProvider.GetStream<UserRegisteredEvent>(this.GetPrimaryKey(), nameof(UserRegisteredEvent));

            var userVerifiedStream = streamProvider.GetStream<UserVerifiedEvent>(this.GetPrimaryKey(), nameof(UserVerifiedEvent));
            await userVerifiedStream.SubscribeAsync(this);
            
            await base.OnActivateAsync();
        }
        
        public async Task RegisterAsync(string name, string surname, string email)
        {
            await _userRegisteredStream.OnNextAsync(new UserRegisteredEvent {Name = name, Surname = surname, Email = email});

            State.Status = UserRegistrationStatusEnum.Blocked;
        }

        public Task<UserRegistrationState> GetAsync()
        {
            return Task.FromResult(State);
        }

        public Task OnNextAsync(UserVerifiedEvent item, StreamSequenceToken token = null)
        {
            State.Status = UserRegistrationStatusEnum.Verified;
            
            return Task.CompletedTask;
        }

        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}