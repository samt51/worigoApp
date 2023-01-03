using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;
using Worigo.Entity.Concrete;

namespace Worigo.Core.FluentValidation
{
    public class CompaniesValidator:AbstractValidator<CompaniesDto>
    {
        public CompaniesValidator()
        {
            RuleFor(x => x.name).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
