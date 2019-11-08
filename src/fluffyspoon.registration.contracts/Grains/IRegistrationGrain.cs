using Orleans;
using System.Threading.Tasks;

namespace fluffyspoon.registration.contracts.Grains
{
    public interface IRegistrationGrain : IGrainWithStringKey
    {
        Task RegisterAsync(string name, string surname);
    }
}
