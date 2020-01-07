using System.ComponentModel.DataAnnotations;

namespace fluffyspoon.registration.ViewModels
{
    public class RegisterUserModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
    }
}