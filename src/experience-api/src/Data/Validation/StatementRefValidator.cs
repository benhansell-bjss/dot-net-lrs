using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class StatementRefValidator : AbstractValidator<StatementRef>
    {
        public StatementRefValidator()
        {
            RuleFor(x => x.ObjectType).Equal(ObjectType.StatementRef);
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}