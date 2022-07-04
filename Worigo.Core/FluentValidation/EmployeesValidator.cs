using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;
namespace Worigo.Core.FluentValidation
{
    public class EmployeesValidator:AbstractValidator<EmployeesDto>
    {
        public EmployeesValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.StartDateOfWork).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.ExitEntryDate).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.FloorNo).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.FloorNo).GreaterThan(0).WithMessage("0'dan büyük olmalı");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
