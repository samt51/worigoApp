using FluentValidation;
using Worigo.Core.Dtos.ListDto;

namespace Worigo.Core.FluentValidation
{
    public class ServiceOfHotelValidator: AbstractValidator<ServiceOfHotelDto>
    {
        public ServiceOfHotelValidator()
        {
            RuleFor(x => x.hotelid).GreaterThan(0).WithMessage("Please A HotelId Enter");
            RuleFor(x => x.serviceid).GreaterThan(0).WithMessage("Please A Serviceid Enter");
        }
    }
}
