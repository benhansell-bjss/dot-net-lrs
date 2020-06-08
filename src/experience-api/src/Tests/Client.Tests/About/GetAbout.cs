using Doctrina.ExperienceApi.Client.Http;
using Doctrina.ExperienceApi.Client.Http.Headers;
using Doctrina.ExperienceApi.Data;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.ExperienceApi.Client.Tests.About
{
    public class GetAbout
    {
        [Fact]
        public async Task Get_About()
        {
            var mockHttp = new MockHttpMessageHandler();

            var authHeader = new BasicAuthHeaderValue("admin", "password");
            var baseAddress = new Uri("http://example.com/xAPI/");

            var about = new Data.About()
            {
                Version = ApiVersion.GetKnownVersions().Select(x => x.Key).ToList(),
                Extensions = new ExtensionsDictionary()
            };

            // Setup a respond for the user api (including a wildcard in the URL)
            var request = mockHttp.When(HttpMethod.Get, $"/xAPI/about")
                .WithHeaders(new Dictionary<string, string>{
                    { ApiHeaders.XExperienceApiVersion, ApiVersion.GetLatest().ToString() },
                    { "Authorization", authHeader.ToString() }
                })
                .Respond("application/json", about.ToJson());

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = baseAddress;

            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.GetAbout();

            result.Version.ShouldBeInOrder(SortDirection.Descending);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }
    }
}
