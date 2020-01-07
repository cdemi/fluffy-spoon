using demofluffyspoon.contracts;
using demofluffyspoon.contracts.Grains;
using demofluffyspoon.contracts.Models;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Grains
{
    [ImplicitStreamSubscription(nameof(UserRegisteredEvent))]
    [ImplicitStreamSubscription(nameof(UserVerifiedEvent))]
    public class UserRegistrationStatusGrain : Grain<RegistrationStatusState>, IUserRegistrationStatusGrain, IAsyncObserver<UserRegisteredEvent>, IAsyncObserver<UserVerifiedEvent>
    {
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);

            // Consumer
            var userRegisteredStream = streamProvider.GetStream<UserRegisteredEvent>(this.GetPrimaryKey(), nameof(UserRegisteredEvent));
            await userRegisteredStream.SubscribeAsync(this);
            
            // Consumer
            var userVerifiedStream = streamProvider.GetStream<UserVerifiedEvent>(this.GetPrimaryKey(), nameof(UserVerifiedEvent));
            await userVerifiedStream.SubscribeAsync(this);

            await base.OnActivateAsync();
        }

        public Task<RegistrationStatusState> GetAsync()
        {
            return Task.FromResult(State);
        }

        public Task OnNextAsync(UserRegisteredEvent item, StreamSequenceToken token = null)
        {
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