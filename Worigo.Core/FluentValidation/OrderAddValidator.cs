using FluentValidation;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class OrderAddValidator: AbstractValidator<OrderAddOrUpdateRequest>
    {
        public OrderAddValidator()
        {
            //RuleFor(x => x.customerId).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
            //RuleFor(x => x.serviceValueId).NotEmpty().NotNull().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
    }
}
