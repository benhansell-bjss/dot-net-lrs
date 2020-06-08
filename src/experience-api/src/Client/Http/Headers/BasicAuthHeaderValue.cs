using System;
using System.Net.Http.Headers;
using System.Text;

namespace Doctrina.ExperienceApi.Client.Http.Headers
{
    public class BasicAuthHeaderValue : AuthenticationHeaderValue
    {
        public BasicAuthHeaderValue(string username, string password)
            : base("Basic", FormatBasicAuth(username, password))
        {
        }

        public static string FormatBasicAuth(string username, string password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            return Convert.ToBase64String(bytes);
        }
    }
}
