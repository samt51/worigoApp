using FluentValidation;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class ManagementAddDtoValidator: AbstractValidator<ManagementAddDto>
    {
        public ManagementAddDtoValidator()
        {
            RuleFor(x => x.email).EmailAddress().WithMessage("Please A Email Adress Enter");
            RuleFor(x => x.password).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x=>x.name).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x=>x.surname).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x=>x.StartDateOfWork).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x=>x.phonenumber).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);   
        }
    }
}
