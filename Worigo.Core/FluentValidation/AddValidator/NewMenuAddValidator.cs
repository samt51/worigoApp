using FluentValidation;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation.AddValidator
{
    public class NewMenuAddValidator: AbstractValidator<AddNewMenuRequest>
    {
        public NewMenuAddValidator()
        {
            RuleFor(x => x.categoryName).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.categoryName).MinimumLength(4).WithMessage("menu name must be at least 5 characters");
            RuleFor(x => x.hotelid).GreaterThan(0).WithMessage("Please Choose A Hotel");
        }
    }
}
