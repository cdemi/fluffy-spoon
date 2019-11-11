using System;

namespace demofluffyspoon.contracts.Models
{
    [Serializable]
    public class UserVerifiedEvent
    {
        public string Email { get; set; }
    }
}