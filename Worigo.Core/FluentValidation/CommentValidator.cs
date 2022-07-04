using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class CommentValidator:AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Point).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Point).InclusiveBetween(1, 5).WithMessage("1 ile 5 arasında puan veriniz");
            RuleFor(x => x.Commentary).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
