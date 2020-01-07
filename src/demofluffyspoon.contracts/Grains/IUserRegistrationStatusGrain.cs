using demofluffyspoon.contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace demofluffyspoon.contracts.Grains
{
    public interface IUserRegistrationStatusGrain : IGrainWithGuidKey
    {
        Task<RegistrationStatusState> GetAsync();
    }
}
