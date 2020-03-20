using System;

namespace DemoFluffySpoon.Contracts.Models
{
    [Serializable]
    public class UserVerificationEvent
    {
        public string Email { get; set; }

        public UserVerificationStatusEnum Status { get; set; } = UserVerificationStatusEnum.NotRegistered;
    }
}