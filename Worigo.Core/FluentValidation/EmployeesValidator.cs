using FluentValidation;
using Worigo.Core.Dtos.Employee.Response;
namespace Worigo.Core.FluentValidation
{
    public class EmployeesValidator:AbstractValidator<EmployeeResponse>
    {
        public EmployeesValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            //RuleFor(x => x.Surname).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            //RuleFor(x => x.ImageUrl).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
