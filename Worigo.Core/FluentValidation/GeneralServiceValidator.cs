using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class GeneralServiceValidator:AbstractValidator<GeneralServiceDto>
    {
        public GeneralServiceValidator()
        {
            RuleFor(x => x.name).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
