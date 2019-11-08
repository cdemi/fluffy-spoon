using fluffyspoon.registration.contracts;
using fluffyspoon.registration.contracts.Grains;
using fluffyspoon.registration.contracts.Streams;
using Orleans;
using Orleans.Streams;
using System.Threading.Tasks;

namespace fluffyspoon.registration.Grains
{
    public class RegistrationGrain : Grain, IRegistrationGrain
    {
        private IAsyncStream<RegisteredUserEvent> _registeredUserEvent;

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Constants.StreamProviderName);
            _registeredUserEvent = streamProvider.GetStream<RegisteredUserEvent>(this.GetPrimaryKey(), nameof(RegisteredUserEvent));
                                   
            await base.OnActivateAsync();
        }
        
        public Task RegisterAsync(string name, string surname, string email)
        {
            _registeredUserEvent.OnNextAsync(new RegisteredUserEvent {Name = name, Surname = surname, Email = email});
            
            return Task.CompletedTask;
        }
    }
}