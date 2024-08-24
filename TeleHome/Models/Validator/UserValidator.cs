using FluentValidation;

namespace TeleHome.Models.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Ad hissəsi boş ola bilməz").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.UserSurname).NotEmpty().WithMessage("Soyad hissəsi boş ola bilməz").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Email hissəsi boş ola bilməz").EmailAddress().WithMessage("Email formatına uyğun deyil").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.UserPhone).NotEmpty().WithMessage("Telefon hissəsi boş ola bilməz");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Şifrə boş ola bilməz").MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır");
            //.Matches("[^a-zA-Z0-9]").WithMessage("Şifrədə mütləq bir xüsusi simvol olmalıdı")
            //.Matches("[A-Z]").WithMessage("Şifrədə 1 böyük hərf olmalıdır")
            //.Matches("[a-z]").WithMessage("Şifrə ən azı 1 kiçik hərfdən ibarət olmalıdır")
            //.Matches("[0-9]").WithMessage("Şifrədə ədəd olmalıdır");
        }
    }
}
      //.Matches("[A-Z]").WithMessage("Şifrədə 1 böyük hərf olmalıdır")
            //.Matches("[a-z]").WithMessage("Şifrə ən azı 1 kiçik hərfdən ibarət olmalıdır")
            //.Matches("[0-9]").WithMessage("Şifrədə ədəd olmalıdır")

//RuleFor(x => x.UserPhone).NotEmpty().WithMessage("Telefon hissəsi boş ola bilməz").Matches(@"^(?:\+994)(?:10|50|51|55|60|70|77|99)\d{7}$").WithMessage("Mobil nömrə formatına uyğun deyil");
