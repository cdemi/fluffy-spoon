using System;

namespace demofluffyspoon.contracts.Models
{
    [Serializable]
    public class UserVerificationEvent
    {
        public string Email { get; set; }

        public UserVerificationStatusEnum Status { get; set; } = UserVerificationStatusEnum.NotRegistered;
    }
}