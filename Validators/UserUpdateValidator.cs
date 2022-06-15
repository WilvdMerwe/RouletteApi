using FluentValidation;
using RouletteApi.Models.Requests;

namespace RouletteApi.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
