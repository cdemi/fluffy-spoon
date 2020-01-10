using System;

namespace demofluffyspoon.contracts.Models
{
    public class RegistrationStatusState
    {
        public UserRegistrationStatusEnum Status { get; set; } = UserRegistrationStatusEnum.NotRegistered;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public double? ResponseTime =>
            UpdatedOn.HasValue 
                ? (UpdatedOn.Value - CreatedOn).TotalMilliseconds 
                : (double?) null;
    }
}