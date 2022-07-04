using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
   public class VertificationCodeValidator:AbstractValidator<VertificationCodeDto>
    {
        public VertificationCodeValidator()
        {
            RuleFor(x => x.roomid).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
