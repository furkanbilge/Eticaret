using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress), Required(ErrorMessage ="E-Posta Boş Bırakılamaz!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Boş Bırakılamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
