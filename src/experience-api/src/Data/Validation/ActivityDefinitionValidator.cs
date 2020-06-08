using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class ActivityDefinitionValidator : AbstractValidator<ActivityDefinition>
    {
        public ActivityDefinitionValidator()
        {
            RuleFor(x => x.Description).SetValidator(new LanguageMapValidator()).When(x => x.Description != null);
            RuleFor(x => x.Name).SetValidator(new LanguageMapValidator()).When(x => x.Name != null);
            RuleFor(x => x.Extensions).SetValidator(new ExtensionsValidator()).When(x => x.Extensions != null);
        }
    }
}
