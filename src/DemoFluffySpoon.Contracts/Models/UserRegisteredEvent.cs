using System;

namespace DemoFluffySpoon.Contracts.Models
{
    [Serializable]
    public class UserRegisteredEvent
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
    }
}