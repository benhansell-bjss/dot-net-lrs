using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class ResultValidator : AbstractValidator<Result>
    {
        public ResultValidator()
        {
            RuleFor(x => x.Score).SetValidator(new ScoreValidator()).When(x => x.Score != null);

            RuleFor(x => x.Extensions).SetValidator(new ExtensionsValidator()).When(x => x.Extensions != null);
        }
    }
}
