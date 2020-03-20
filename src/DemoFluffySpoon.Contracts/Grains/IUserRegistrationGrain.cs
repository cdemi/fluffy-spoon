using System;
using System.Threading.Tasks;
using Orleans;

namespace DemoFluffySpoon.Contracts.Grains
{
    public interface IUserRegistrationGrain : IGrainWithStringKey
    {
        Task<Guid?> RegisterAsync(string name, string surname);
    }
}
