using FluentValidation;
using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class DepartmanValidator:AbstractValidator<DepartmentAddOrUpdateRequest>
    {
        public DepartmanValidator()
        {
            RuleFor(x => x.DepartmanName).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage("Picture Required");
        }
    }
}
