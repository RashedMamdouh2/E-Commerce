using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class SignupViewModel
    {
        [DisplayName("User Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Re-Enter Your Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="The Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
     
    }
}
