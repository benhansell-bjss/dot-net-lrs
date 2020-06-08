using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.InteractionTypes
{
    public class Numeric : InteractionActivityDefinitionBase
    {
        public Numeric() { }

        public Numeric(JToken jobj, ApiVersion version) : base(jobj, version)
        {
        }

        protected override InteractionType INTERACTION_TYPE => InteractionType.Numeric;
    }
}
