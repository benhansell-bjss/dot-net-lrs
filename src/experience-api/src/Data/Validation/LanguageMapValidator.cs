using Doctrina.ExperienceApi.Data.Helpers;
using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class LanguageMapValidator : AbstractValidator<LanguageMap>
    {
        public LanguageMapValidator()
        {
            RuleForEach(x => x).Must(x => CultureHelper.IsValidCultureName(x.Key))
                .WithMessage("Invalid culture name.");
        }
    }
}
