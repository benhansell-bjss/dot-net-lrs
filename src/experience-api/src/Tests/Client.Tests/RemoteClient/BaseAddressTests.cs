using Doctrina.ExperienceApi.Client.Http.Headers;
using Doctrina.ExperienceApi.Data;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using Xunit;

namespace Doctrina.ExperienceApi.Client.Tests.RemoteClient
{
    public class BaseAddressTests
    {
        [Fact]
        public void Ensure_BaseAddress_EndWith_Slash()
        {
            var mockHttp = new MockHttpMessageHandler();

            var authHeader = new BasicAuthHeaderValue("admin", "password");
            var baseAddress = new Uri("http://example.com/xAPI");

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = baseAddress;

            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            client.HttpClient.BaseAddress.ToString().ShouldBe("http://example.com/xAPI/");
        }

        [Fact]
        public void Fix_Ensure_BaseAddress_EndWith_Slash()
        {
            var mockHttp = new MockHttpMessageHandler();

            var authHeader = new BasicAuthHeaderValue("admin", "password");
            var baseAddress = new Uri("http://example.com/xAPI/");

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = baseAddress;

            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            client.HttpClient.BaseAddress.ToString().ShouldBe("http://example.com/xAPI/");
        }
    }
}
