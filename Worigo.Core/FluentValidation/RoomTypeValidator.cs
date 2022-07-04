using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class RoomTypeValidator:AbstractValidator<RoomTypeDto>
    {
        public RoomTypeValidator()
        {
            RuleFor(x => x.typeName).NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
