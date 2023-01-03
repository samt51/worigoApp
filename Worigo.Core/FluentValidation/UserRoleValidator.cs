using FluentValidation;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class UserRoleValidator:AbstractValidator<UserRoleRequest>
    {
        public UserRoleValidator()
        {
            RuleFor(x => x.RoleName).NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
