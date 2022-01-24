using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First Name field is required")]
        public string Firstname { get; set; }
        //[Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Surname field is required")]
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public IFormFile Files { get; set; }
        public FileViewModel CV { get; set; }
    }
    
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
