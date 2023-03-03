using Core.Services;
using FluentValidation;

namespace Core.Validations;

public class AccountValidator : AbstractValidator<InsertAccountRequest>
{

    public AccountValidator()
    {
        RuleFor(x => x.Phone).NotNull();
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.Phone).Must(BeAValidPhone)
                             .WithMessage("Invalid Phone.");
        // Only support +57 prefix plus a combination of numbers from 0 to 9 with a minimum of 6 and maximum of 14 digits
        RuleFor(x => x.Phone).Matches(@"^[+]57{1}[0-9]{6,14}")
                             .WithMessage("Please specify a valid phone country");

        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Address).NotEmpty();
    }

    private bool BeAValidPhone(string phone)
    {
        return double.TryParse(phone, out var val) && val > 0;
    }
}