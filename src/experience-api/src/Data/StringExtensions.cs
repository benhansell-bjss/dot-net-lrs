namespace Doctrina.ExperienceApi.Data
{
    public static class StringExtensions
    {
        public static string EnsureEndsWith(this string str, string endWith)
        {
            if (!str.EndsWith(endWith))
            {
                return str + endWith;
            }

            return str;
        }
    }
}
