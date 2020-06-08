using Doctrina.ExperienceApi.Client.Http.Headers;
using Doctrina.ExperienceApi.Data;
using RichardSzalay.MockHttp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.ExperienceApi.Client.Tests.Statements
{
    public class GetStatement
    {
        public readonly BasicAuthHeaderValue AuthHeader = new BasicAuthHeaderValue("admin", "password");
        public readonly Uri BaseAddress = new Uri("http://example.com/xAPI");

        [Fact]
        public async Task Get_Statement_By_Id_Format_Exact()
        {
            var mockHttp = new MockHttpMessageHandler();
            var statement = new Statement("{'id':'c70c2b85-c294-464f-baca-cebd4fb9b348','timestamp':'2014-12-29T12:09:37.468Z','actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}");

            var request = mockHttp.Expect(HttpMethod.Get, $"{BaseAddress}/statements")
               .WithQueryString(new Dictionary<string, string>() {
                    { "statementId", statement.Id.Value.ToString() },
                    { "format", ResultFormat.Exact.ToString() }
               })
                   .Respond("application/json", statement.ToJson());

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = BaseAddress;

            var client = new LRSClient(AuthHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.GetStatement(statement.Id.Value);

            result.Id.Value.ShouldBe(statement.Id.Value);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }

        [Fact]
        public async Task Get_Statement_By_Id_Format_Cannonical()
        {
            var mockHttp = new MockHttpMessageHandler();
            var statement = new Statement("{'id':'c70c2b85-c294-464f-baca-cebd4fb9b348','timestamp':'2014-12-29T12:09:37.468Z','actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}");

            var request = mockHttp.Expect(HttpMethod.Get, $"{BaseAddress}/statements")
                .WithQueryString(new Dictionary<string, string>() {
                    { "statementId", statement.Id.Value.ToString() },
                    { "format", ResultFormat.Canonical.ToString() }
                })
                .Respond("application/json", statement.ToJson(ResultFormat.Canonical));

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = BaseAddress;

            var client = new LRSClient(AuthHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.GetStatement(
                statement.Id.Value,
                format: ResultFormat.Canonical
            );

            result.Id.Value.ShouldBe(statement.Id.Value);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }

        [Fact]
        public async Task Get_Statement_By_Id_Format_Ids()
        {
            var mockHttp = new MockHttpMessageHandler();
            var statement = new Statement("{'id':'c70c2b85-c294-464f-baca-cebd4fb9b348','timestamp':'2014-12-29T12:09:37.468Z','actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}");

            var request = mockHttp.Expect(HttpMethod.Get, $"{BaseAddress}/statements")
                .WithQueryString(new Dictionary<string, string>() {
                    { "statementId", statement.Id.Value.ToString() },
                    { "format", ResultFormat.Ids.ToString() }
                })
                .Respond("application/json", statement.ToJson(ResultFormat.Ids));

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = BaseAddress;

            var client = new LRSClient(AuthHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.GetStatement(
                statement.Id.Value,
                format: ResultFormat.Ids
            );

            result.Id.Value.ShouldBe(statement.Id.Value);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }
    }
}
