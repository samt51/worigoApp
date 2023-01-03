using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class RoomValidator : AbstractValidator<RoomDto>
    {
        public RoomValidator()
        {
            RuleFor(x => x.RoomTypeid).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.NumberOfBeds).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.NumberOfBeds).GreaterThan(0).WithMessage(priceValidator());
            RuleFor(x => x.RoomNo).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.RoomNo).GreaterThan(0).WithMessage(priceValidator());
           
            RuleFor(x => x.Description).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Price).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(priceValidator());   
        }
        public static string priceValidator()
        {
            return "Fiyat 0'dan büyük olmalı";
        }
    }
}
