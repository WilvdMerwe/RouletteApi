using FluentValidation;
using RouletteApi.Models.Requests;

namespace RouletteApi.Validation
{
    public class UserCreateValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("{PropertyName} cannot be empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid");
        }
    }
}
