using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class ServiceValueValidator:AbstractValidator<ServiceValueDto>
    {
        public ServiceValueValidator()
        {
            RuleFor(x => x.value).NotNull().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x=>x.Serviceid).NotEmpty().NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
