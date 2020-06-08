using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.HomePage).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
