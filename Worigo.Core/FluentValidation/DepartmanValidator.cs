using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class DepartmanValidator:AbstractValidator<DepartmanDto>
    {
        public DepartmanValidator()
        {
            RuleFor(x => x.DepartmanName).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Hotelid).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Hotelid).GreaterThan(0).WithMessage("Hotelid bir hotel id içermeli");
        }
    }
}
