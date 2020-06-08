using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Doctrina.ExperienceApi.Data
{
    public class About
    {
        public IEnumerable<string> Version { get; set; }

        public ExtensionsDictionary Extensions { get; set; }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("version");
                writer.WriteStartArray();
                foreach (var strVersion in Version)
                {
                    writer.WriteValue(strVersion);
                }
                writer.WriteEndArray();
                writer.WritePropertyName("extensions");
                if (Extensions != null)
                {
                    writer.WriteRaw(Extensions.ToJson());
                }
                writer.WriteEndObject();
            }

            return sb.ToString();
        }
    }
}
