using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.InteractionTypes
{
    public class FillIn : InteractionActivityDefinitionBase
    {
        protected override InteractionType INTERACTION_TYPE => InteractionType.FillIn;

        public FillIn()
        {
        }

        public FillIn(JToken jobj, ApiVersion version) : base(jobj, version)
        {
        }

    }
}
