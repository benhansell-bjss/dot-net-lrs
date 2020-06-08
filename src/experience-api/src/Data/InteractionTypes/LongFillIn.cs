using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.InteractionTypes
{
    public class LongFillIn : InteractionActivityDefinitionBase
    {
        protected override InteractionType INTERACTION_TYPE => InteractionType.LongFillIn;

        public LongFillIn()
        {
        }
        public LongFillIn(JToken jobj, ApiVersion version) : base(jobj, version)
        {
        }
    }
}
