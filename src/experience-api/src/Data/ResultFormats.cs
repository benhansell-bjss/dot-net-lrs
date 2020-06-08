using System.Runtime.Serialization;

namespace Doctrina.ExperienceApi.Data
{
    public enum ResultFormat
    {
        [EnumMember(Value = "ids")]
        Ids = 0,

        [EnumMember(Value = "exact")]
        Exact = 1,

        [EnumMember(Value = "canonical")]
        Canonical = 2
    }
}
