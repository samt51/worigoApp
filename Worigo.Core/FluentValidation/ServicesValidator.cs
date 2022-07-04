using FluentValidation;
using Worigo.Core.Dtos.ListDto;

namespace Worigo.Core.FluentValidation
{
    public class ServicesValidator:AbstractValidator<ServicesDto>
    {
        public ServicesValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(requiredmessage());
        }
        public static string requiredmessage()
        {
            return "Zorunlu Alan";
        }
    }
}
