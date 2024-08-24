using System.ComponentModel.DataAnnotations;

namespace TeleHome
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }
}
