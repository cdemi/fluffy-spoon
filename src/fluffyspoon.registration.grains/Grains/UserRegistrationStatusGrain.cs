using demofluffyspoon.contracts;
using demofluffyspoon.contracts.Grains;
using demofluffyspoon.contracts.Models;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace fluffyspoon.registration.grains.Grains
{
    [ImplicitStreamSubscription(nameof(UserRegisteredEvent))]
    [ImplicitStreamSubscription(nameof(UserVerificationEvent))]
    public class UserRegistrationStatusGrain : Grain<RegistrationStatusState>, IUserRegistrationStatusGrain, IAsyncObserver<UserRegisteredEvent>, IAsyncObserver<UserVerificationEvent>
    {
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);

            // Consumer
            var userRegisteredStream = streamProvider.GetStream<UserRegisteredEvent>(this.GetPrimaryKey(), nameof(UserRegisteredEvent));
            await userRegisteredStream.SubscribeAsync(this);
            
            // Consumer
            var userVerificationStream = streamProvider.GetStream<UserVerificationEvent>(this.GetPrimaryKey(), nameof(UserVerificationEvent));
            await userVerificationStream.SubscribeAsync(this);

            await base.OnActivateAsync();
        }

        public Task<RegistrationStatusState> GetAsync()
        {
            return Task.FromResult(State);
        }

        public Task OnNextAsync(UserRegisteredEvent item, StreamSequenceToken token = null)
        {
            State.Status = UserRegistrationStatusEnum.Pending;
            
            return Task.CompletedTask;
        }
        
        public Task OnNextAsync(UserVerificationEvent item, StreamSequenceToken token = null)
        {
            State.Status = item.Status switch
            {
                UserVerificationStatusEnum.Verified => UserRegistrationStatusEnum.Verified,
                UserVerificationStatusEnum.Blocked => UserRegistrationStatusEnum.Blocked,
                _ => State.Status
            };

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