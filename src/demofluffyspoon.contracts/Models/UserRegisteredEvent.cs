using System;

namespace demofluffyspoon.contracts.Models
{
    [Serializable]
    public class UserRegisteredEvent
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
    }
}