using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTO
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public FileModel CV { get; set; }
    }
    
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
    
    public class UserClaimsModel
    {
        public int MyProperty { get; set; }
    }
}
