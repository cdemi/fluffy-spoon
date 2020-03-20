using System;
using System.Threading.Tasks;
using DemoFluffySpoon.Contracts;
using DemoFluffySpoon.Contracts.Grains;
using DemoFluffySpoon.Contracts.Models;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Streams;

namespace DemoFluffySpoon.Registration.Grains.Grains
{
    [ImplicitStreamSubscription(nameof(UserRegisteredEvent))]
    [ImplicitStreamSubscription(nameof(UserVerificationEvent))]
    [Reentrant]
    public class UserRegistrationStatusGrain : Grain, IUserRegistrationStatusGrain, IAsyncObserver<UserRegisteredEvent>, IAsyncObserver<UserVerificationEvent>
    {
        private readonly IPersistentState<RegistrationStatusState> _registrationStatusState;

        public UserRegistrationStatusGrain([PersistentState(nameof(RegistrationStatusState))] IPersistentState<RegistrationStatusState> registrationStatusState)
        {
            _registrationStatusState = registrationStatusState;
        }

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
            return Task.FromResult(_registrationStatusState.State);
        }

        public Task OnNextAsync(UserRegisteredEvent item, StreamSequenceToken token = null)
        {
            _registrationStatusState.State.Status = UserRegistrationStatusEnum.Pending;
            
            return Task.CompletedTask;
        }
        
        public Task OnNextAsync(UserVerificationEvent item, StreamSequenceToken token = null)
        {
            _registrationStatusState.State.Status = item.Status switch
            {
                UserVerificationStatusEnum.Verified => UserRegistrationStatusEnum.Verified,
                UserVerificationStatusEnum.Blocked => UserRegistrationStatusEnum.Blocked,
                _ => _registrationStatusState.State.Status
            };
            
            _registrationStatusState.State.UpdatedOn = DateTime.UtcNow;

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