using Doctrina.ExperienceApi.Data.Json;
using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class GuidFormatException : JsonTokenModelException
    {
        public GuidFormatException(JToken token, string guid)
            : base(token, $"'{guid}' is not a valid UUID.")
        {
        }
    }
}
