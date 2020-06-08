namespace Doctrina.ExperienceApi.Data
{
    public interface IActivity
    {
        Iri Id { get; set; }
        ActivityDefinition Definition { get; set; }
    }
}