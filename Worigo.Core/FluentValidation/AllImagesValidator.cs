using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class AllImagesValidator:AbstractValidator<AllImagesDto>
    {
        public AllImagesValidator()
        {
            RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
