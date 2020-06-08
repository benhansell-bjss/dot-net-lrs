using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.InteractionTypes
{
    public class Other : InteractionActivityDefinitionBase
    {
        public Other()
        {
        }

        public Other(JToken jobj, ApiVersion version) : base(jobj, version)
        {
        }

        protected override InteractionType INTERACTION_TYPE => InteractionType.Other;
    }
}
