using FluentValidation;

namespace TeleHome.Models.Validator
{
    public class SubscribeValidator : AbstractValidator<Subscribe>
    {
        public SubscribeValidator()
        {
            RuleFor(x => x.SubscribeEmail).NotEmpty().WithMessage("Email hissəsi boş ola bilməz").EmailAddress().WithMessage("Email formatına uyğun deyil").MinimumLength(3).WithMessage("Minimum 3 simvol olmalıdır").MaximumLength(30).WithMessage("Maksimum 30 simvol olmalıdır");
        }
    }
}
