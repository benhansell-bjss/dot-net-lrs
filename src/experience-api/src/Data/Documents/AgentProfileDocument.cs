namespace Doctrina.ExperienceApi.Data.Documents
{
    public class AgentProfileDocument : Document
    {
        public string ProfileId { get; set; }
        public Agent Agent { get; set; }
    }
}
