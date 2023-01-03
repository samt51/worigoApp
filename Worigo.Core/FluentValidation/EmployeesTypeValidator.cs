using FluentValidation;
using Worigo.Core.Dtos.EmployeeType.Request;

namespace Worigo.Core.FluentValidation
{
    public class EmployeesTypeValidator: AbstractValidator<EmployeeTypeAddOrUpdateRequest>
    {
        public EmployeesTypeValidator()
        {
            //RuleFor(x=>x.TypeName).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);

        }
    }
}
