using Doctrina.ExperienceApi.Data.Json;
using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class LanguageTagException : JsonTokenModelException
    {
        public LanguageTagException(JToken token, string cultureName)
            : base(token, $"'{cultureName}' is not a valid RFC5646 Language Tag.")
        {
        }
    }
}
