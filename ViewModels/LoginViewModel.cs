using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Remember me for a month")]
        public bool RememberMe { get; set; }

    }
}
