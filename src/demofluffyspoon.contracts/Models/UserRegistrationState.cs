namespace demofluffyspoon.contracts.Models
{
    public class UserRegistrationState
    {
        public UserRegistrationStatusEnum Status { get; set; } = UserRegistrationStatusEnum.Unverified;
    }
}