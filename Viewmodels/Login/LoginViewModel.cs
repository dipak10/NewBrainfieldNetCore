using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Viewmodels.Login
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
