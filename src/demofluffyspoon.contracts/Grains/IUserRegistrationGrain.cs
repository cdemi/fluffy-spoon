using Orleans;
using System;
using System.Threading.Tasks;

namespace demofluffyspoon.contracts.Grains
{
    public interface IUserRegistrationGrain : IGrainWithStringKey
    {
        Task<Guid?> RegisterAsync(string name, string surname);
    }
}
