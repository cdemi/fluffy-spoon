using System;

namespace DemoFluffySpoon.Contracts.Models
{
    public class RegistrationState
    {
        public bool IsRegistered { get; set; }
        
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}