using Doctrina.ExperienceApi.Client.Http;
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
    public class PostStatements
    {
        public static Uri baseAddress = new Uri("http://example.com/xAPI");

        [Fact]
        public async Task Post_Single_Statement()
        {
            var mockHttp = new MockHttpMessageHandler();

            var statement = new Statement("{'id':'c70c2b85-c294-464f-baca-cebd4fb9b348','timestamp':'2014-12-29T12:09:37.468Z','actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}");

            var authHeader = new BasicAuthHeaderValue("admin", "password");
            // Setup a respond for the user api (including a wildcard in the URL)
            var request = mockHttp.When(HttpMethod.Put, $"{baseAddress}/statements")
                .WithQueryString("statementId", statement.Id?.ToString())
                .WithHeaders(new Dictionary<string, string>{
                    { ApiHeaders.XExperienceApiVersion, ApiVersion.GetLatest().ToString() },
                    { ApiHeaders.Authorization, authHeader.ToString() }
                })
                .Respond(MediaTypes.Application.Json, statement.ToJson());

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = baseAddress;

            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.SaveStatement(statement);

            result.Id.ShouldBe(statement.Id);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }

        [Fact]
        public async Task Post_Single_Statement_With_Attachment()
        {
            var mockHttp = new MockHttpMessageHandler();

            var statement = new Statement("{'actor':{'objectType':'Agent','mbox':'mailto:example@example.com','name':'Test User'},'verb':{'id':'http://adlnet.gov/expapi/verbs/experienced','display':{'en-US':'experienced'}},'object':{'id':'http://example.com/xAPI/activities/myactivity','objectType':'Activity'}}");

            statement.Attachments = new AttachmentCollection();
            var attachment = new Attachment()
            {
                ContentType = "",
                Description = new LanguageMap() { { "en-GB", "Certficate of completion" } },
                Display = new LanguageMap()
                {
                    { "en-GB", "Certificate" },
                }
            };
            // TODO: Attachment payload
            // attachment.SetPayload(File.OpenRead("~/Files/certificates.pdf"));

            statement.Attachments.Add(attachment);

            statement.Stamp();

            var authHeader = new BasicAuthHeaderValue("admin", "password");

            // Setup a respond for the user api (including a wildcard in the URL)
            var request = mockHttp.When(HttpMethod.Put, $"{baseAddress}/statements")
                .WithQueryString("statementId", statement.Id?.ToString())
                .WithHeaders(new Dictionary<string, string>{
                    { ApiHeaders.XExperienceApiVersion, ApiVersion.GetLatest().ToString() },
                    { ApiHeaders.Authorization, authHeader.ToString() }
                })
                .Respond(MediaTypes.Multipart.Mixed, statement.ToJson());

            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = baseAddress;

            var client = new LRSClient(authHeader, ApiVersion.GetLatest(), httpClient);

            var result = await client.SaveStatement(statement);

            result.Id.ShouldBe(statement.Id);

            mockHttp.GetMatchCount(request).ShouldBe(1);
        }

        [Fact]
        public async Task Post_Single_Statement_With_Multiple_Attachments()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }


        [Fact]
        public async Task Post_Multiple_Statements()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        [Fact]
        public async Task Post_Multiple_Statements_With_Single_Attachment()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        [Fact]
        public async Task Post_Multiple_Statements_With_Multiple_Attachment()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
