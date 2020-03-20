using System.ComponentModel.DataAnnotations;

namespace DemoFluffySpoon.Registration.ViewModels
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