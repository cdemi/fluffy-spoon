using Orleans;
using System.Threading.Tasks;

namespace fluffyspoon.registration.contracts.Grains
{
    public interface IRegistrationGrain : IGrainWithGuidKey
    {
        Task RegisterAsync(string name, string surname, string email);
    }
}
