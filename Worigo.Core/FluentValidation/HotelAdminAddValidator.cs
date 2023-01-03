using FluentValidation;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class HotelAdminAddValidator: AbstractValidator<AddHotelAdminModelDto>
    {
        public HotelAdminAddValidator()
        {
            RuleFor(x => x.name).NotNull().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.surname).NotNull().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.phonenumber).NotNull().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.email).EmailAddress().WithMessage("Please a Email Adress Enter");
            RuleFor(x => x.password).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);

        }
    }
}
