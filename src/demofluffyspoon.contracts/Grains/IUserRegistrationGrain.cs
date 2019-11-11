using demofluffyspoon.contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace demofluffyspoon.contracts.Grains
{
    public interface IUserRegistrationGrain : IGrainWithGuidKey
    {
        Task RegisterAsync(string name, string surname, string email);
        
        Task<UserRegistrationState> GetAsync();
    }
}
