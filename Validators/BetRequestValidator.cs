using FluentValidation;
using RouletteApi.Models.Requests;

namespace RouletteApi.Validators
{
    public class BetRequestValidator : AbstractValidator<BetRequest>
    {
        public BetRequestValidator()
        {
            RuleFor(x => x.UserId).Must(userId => userId > 0).WithMessage("{PropertyName} must be bigger than 0");
            RuleFor(x => x.Amount).Must(amount => amount > 0).WithMessage("{PropertyName} must be bigger than 0");
            RuleFor(x => x.Numbers).NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
