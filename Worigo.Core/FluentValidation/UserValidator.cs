using FluentValidation;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class UserValidator: AbstractValidator<AddHotelAdminModelDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.email).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.password).NotNull().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.email).EmailAddress().WithMessage("Please a email Enter");
        }
    }
}
