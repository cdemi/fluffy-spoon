using System.Threading.Tasks;
using DemoFluffySpoon.Contracts.Models;
using Orleans;

namespace DemoFluffySpoon.Contracts.Grains
{
    public interface IUserRegistrationStatusGrain : IGrainWithGuidKey
    {
        Task<RegistrationStatusState> GetAsync();
    }
}
