using FluentValidation;

namespace TeleHome.Models.Validator
{
    public class ContactValidator :AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("Ad hissəsi boş ola bilməz").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.ContactEmail).NotEmpty().WithMessage("Email hissəsi boş ola bilməz").EmailAddress().WithMessage("Email formatına uyğun deyil").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.ContactTitle).NotEmpty().WithMessage("Başlıq hissəsi boş ola bilməz").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
            RuleFor(x => x.ContactMessage).NotEmpty().WithMessage("Mesaj hissəsi boş ola bilməz").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(1000).WithMessage("Maksimum 1000 simvol olmalıdır");
        }
    }
}
