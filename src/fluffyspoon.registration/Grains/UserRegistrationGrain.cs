using demofluffyspoon.contracts;
using fluffyspoon.registration.contracts.Grains;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Grains
{
    public class UserRegistrationGrain : Grain<UserRegistrationState>, IUserRegistrationGrain, IAsyncObserver<UserVerifiedEvent>
    {
        private IAsyncStream<UserRegisteredEvent> _userRegisteredStream;
        private IAsyncStream<UserVerifiedEvent> _userVerifiedStream;

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);
            _userVerifiedStream = streamProvider.GetStream<UserVerifiedEvent>(this.GetPrimaryKey(), nameof(UserVerifiedEvent));
            _userRegisteredStream = streamProvider.GetStream<UserRegisteredEvent>(this.GetPrimaryKey(), nameof(UserRegisteredEvent));

            await _userVerifiedStream.SubscribeAsync(this);
            
            await base.OnActivateAsync();
        }
        
        public Task RegisterAsync(string name, string surname, string email)
        {
            _userRegisteredStream.OnNextAsync(new UserRegisteredEvent {Name = name, Surname = surname, Email = email});
            State.Status = UserRegistrationStatusEnum.Blocked;
            
            return Task.CompletedTask;
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