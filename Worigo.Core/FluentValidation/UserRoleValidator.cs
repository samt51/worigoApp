using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class UserRoleValidator:AbstractValidator<UserRoleDto>
    {
        public UserRoleValidator()
        {
            RuleFor(x => x.RoleName).NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
