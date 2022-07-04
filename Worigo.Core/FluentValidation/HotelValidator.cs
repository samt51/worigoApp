using FluentValidation;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Enum;

namespace Worigo.Core.FluentValidation
{
    public class HotelValidator:AbstractValidator<HotelDto>
    {
        public HotelValidator()
        {
            RuleFor(x => x.HotelName).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage) ;
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Adress).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-posta adresinizi kontrol ediniz.");
            RuleFor(x=>x.NumberOfStar).InclusiveBetween(1, 5).WithMessage("1 ile 5 arasında bir yıldız sayısı giriniz.");
            RuleFor(x => x.NumberOfStar).NotEmpty().WithMessage(MessageEnum.ValidatorRequiredMessage);
        }
        public static string buyuk()
        {
            return "0 ' dan büyük olmalı ";
        }
    }
}
