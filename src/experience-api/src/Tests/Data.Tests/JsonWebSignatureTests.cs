using Doctrina.ExperienceApi.Data.Security.Cryptography;
using Doctrina.ExperienceApi.Data.Validation;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class JsonWebSignatureTests
    {
        [Fact]
        public void Base64EncodeOrDecode()
        {
            string input = "{\"lorem\":\"ipsum\"}";
            string encoded = JsonWebSignature.Base64UrlEncode(Encoding.UTF8.GetBytes(input));
            string decoded = Encoding.UTF8.GetString(JsonWebSignature.Base64UrlDecode(encoded));
            decoded.ShouldBe(input);
        }

        /// <summary>
        /// rejects a signed statement with a malformed signature - bad content type
        /// </summary>
        [Fact]
        public void RejectMalformedSignature_BadContentType()
        {
            string jsonStmt = "{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"},\"id\":\"563e9437-3b5e-4758-8b19-0553f70772e9\",\"attachments\":[{\"usageType\":\"http://adlnet.gov/expapi/attachments/signature\",\"display\":{\"en-US\":\"Signed by the Test Suite\"},\"description\":{\"en-US\":\"Signed by the Test Suite\"},\"contentType\":\"text/plain; charset=ascii\",\"length\":796,\"sha2\":\"f97499f95f2a49e74881fade641b0b4562630ab6a5a0d4cfee2d88a036ea68a1\"}]}";
            string base64UrlEncodedString = "eyJhbGciOiJSUzI1NiJ9.eyJhY3RvciI6eyJvYmplY3RUeXBlIjoiQWdlbnQiLCJuYW1lIjoieEFQSSBtYm94IiwibWJveCI6Im1haWx0bzp4YXBpQGFkbG5ldC5nb3YifSwidmVyYiI6eyJpZCI6Imh0dHA6Ly9hZGxuZXQuZ292L2V4cGFwaS92ZXJicy9hdHRlbmRlZCIsImRpc3BsYXkiOnsiZW4tR0IiOiJhdHRlbmRlZCIsImVuLVVTIjoiYXR0ZW5kZWQifX0sIm9iamVjdCI6eyJvYmplY3RUeXBlIjoiQWN0aXZpdHkiLCJpZCI6Imh0dHA6Ly93d3cuZXhhbXBsZS5jb20vbWVldGluZ3Mvb2NjdXJhbmNlcy8zNDUzNCJ9LCJpZCI6IjU2M2U5NDM3LTNiNWUtNDc1OC04YjE5LTA1NTNmNzA3NzJlOSJ9.uJ9wAMhZbQnw9hnRc6di9tFQbtXf08Bu2tE3yaMhhgNypfMDaVCm2Ol_3kwZqWOTgdgQ8C9hDDRIOp2Xbac8K3DMlXEO9VydqZEBvH4KhKsm3x0Wu2WnYuGwH3bbN0ke0yk6HqblmsGewyaEN9ceBI-d0gLBtyBeBEi_HNb9wEcgwa9hrbPv2SZlXkoJ8c1-_-e6iBoknLVT2OyJ1x-bokyVFfpB2qyVpZ3hKzi30sFHUoMpCOhg9k8YpMoZQIbEt9mW4-jcDb23s9KCq4GZBhKme1q3wypxhFRcxXAAuLp9rXE_JRtrmIpuoI1Jtxr0GhlbWV1BE-7AweeSit1Zhw";

            var statement = new Statement(jsonStmt);
            var attachment = statement.GetAttachmentByHash("f97499f95f2a49e74881fade641b0b4562630ab6a5a0d4cfee2d88a036ea68a1");
            attachment.SetPayload(base64UrlEncodedString);
            var validator = new StatementValidator();
            var results = validator.Validate(statement);

            var jws = JsonWebSignature.Parse(base64UrlEncodedString);

            results.IsValid.ShouldBeFalse();
        }

        /// <summary>
        /// rejects a signed statement with a malformed signature - bad JWS
        /// </summary>
        [Fact]
        public void RefectMalformedSignature_BadJWS()
        {
            string jsonStmt = "{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"},\"id\":\"73db416a-b682-4d7f-8694-3de4829b5f2f\",\"attachments\":[{\"usageType\":\"http://adlnet.gov/expapi/attachments/signature\",\"display\":{\"en-US\":\"Signed by the Test Suite\"},\"description\":{\"en-US\":\"Signed by the Test Suite\"},\"contentType\":\"application/octet-stream\",\"length\":796,\"sha2\":\"7ad3cc1bda4611effb53a753a708d1b9333a7b4a3f8580b76a737151813af264\"}]}";
            string base64UrlEncodedString = "eyJhbGciOiJSUzI1NiJ9.eydhY3RvciI6eyJvYmplY3RUeXBlIjoiQWdlbnQiLCJuYW1lIjoieEFQSSBtYm94IiwibWJveCI6Im1haWx0bzp4YXBpQGFkbG5ldC5nb3YifSwidmVyYiI6eyJpZCI6Imh0dHA6Ly9hZGxuZXQuZ292L2V4cGFwaS92ZXJicy9hdHRlbmRlZCIsImRpc3BsYXkiOnsiZW4tR0IiOiJhdHRlbmRlZCIsImVuLVVTIjoiYXR0ZW5kZWQifX0sIm9iamVjdCI6eyJvYmplY3RUeXBlIjoiQWN0aXZpdHkiLCJpZCI6Imh0dHA6Ly93d3cuZXhhbXBsZS5jb20vbWVldGluZ3Mvb2NjdXJhbmNlcy8zNDUzNCJ9LCJpZCI6IjczZGI0MTZhLWI2ODItNGQ3Zi04Njk0LTNkZTQ4MjliNWYyZiJ9.emQMcqSdOZf3gClSKkI45ipp-U3oZlPQZ30YVQfYgDgZ-vu5AY0eF68mWDgMKU8eJh7ptJUDuKlhB8Ylx4QB0l5AlCT34UWDYD5b8SXY9wnaZF2rzQdHwhtauJuF4kyVUdZOE9xiYH5ui8EZEqnf8hXtaHIdrjJX_HvLQ0-18hetxZBZROqqiUNhWSpSokhSDHNVYGStYWiods-ZxpdkaNyktfbRotNiD93MNmwREfDBghrgZ_jegbXaN6luUdaFUkgu6mdHmgIs_EBLRW2EZBD6E22dnHy8SqKp1EeNe8oiq3EviMWxMzZa1ZLR3NBEaTgvc-L0p-URF4SkdFl9EQ";

            var statement = new Statement(jsonStmt);
            var attachment = statement.GetAttachmentByHash("7ad3cc1bda4611effb53a753a708d1b9333a7b4a3f8580b76a737151813af264");
            attachment.SetPayload(base64UrlEncodedString);
            var validator = new StatementValidator();
            var results = validator.Validate(statement);

            results.IsValid.ShouldBeFalse();
        }

        /// <summary>
        /// rejects statement with invalid JSON serialization
        /// </summary>
        [Fact]
        public void Reject()
        {
            string jsonStmt = "{\"actor\":{\"objectType\":\"Agent\",\"name\":\"xAPI mbox\",\"mbox\":\"mailto:xapi@adlnet.gov\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/attended\",\"display\":{\"en-GB\":\"attended\",\"en-US\":\"attended\"}},\"object\":{\"objectType\":\"Activity\",\"id\":\"http://www.example.com/meetings/occurances/34534\"},\"id\":\"2f5de0db-cc2c-480e-bf87-00e67a762cf6\",\"attachments\":[{\"usageType\":\"http://adlnet.gov/expapi/attachments/signature\",\"display\":{\"en-US\":\"Signed by the Test Suite\"},\"description\":{\"en-US\":\"Signed by the Test Suite\"},\"contentType\":\"application/octet-stream\",\"length\":796,\"sha2\":\"0965225653b117b17ecd7556eda9ab2c5ea627c393bb0fb877d43b41f533d493\"}]}";
            string base64UrlEncodedString = "eyJhbGciOiJSUzI1NiJ9.eydhY3RvciI6eyJvYmplY3RUeXBlIjoiQWdlbnQiLCJuYW1lIjoieEFQSSBtYm94IiwibWJveCI6Im1haWx0bzp4YXBpQGFkbG5ldC5nb3YifSwidmVyYiI6eyJpZCI6Imh0dHA6Ly9hZGxuZXQuZ292L2V4cGFwaS92ZXJicy9hdHRlbmRlZCIsImRpc3BsYXkiOnsiZW4tR0IiOiJhdHRlbmRlZCIsImVuLVVTIjoiYXR0ZW5kZWQifX0sIm9iamVjdCI6eyJvYmplY3RUeXBlIjoiQWN0aXZpdHkiLCJpZCI6Imh0dHA6Ly93d3cuZXhhbXBsZS5jb20vbWVldGluZ3Mvb2NjdXJhbmNlcy8zNDUzNCJ9LCJpZCI6IjJmNWRlMGRiLWNjMmMtNDgwZS1iZjg3LTAwZTY3YTc2MmNmNiJ9.cJMGnME2CxGAoLR2mMZ8e9qfWkr5mO1v-59XK3nRWw3yO7X0E8q70VF3bRvGMdhIW2PtU1nM9CHzexox13MytpXTz9eYPb836pJttJuQuQhnSSTpPojwrn2x4H4hEfRyHMkj2Km__Hf2HQSKAD2UbcUn47QswjKCVOZK6U7LWG9nEFfZdS-_CpxyuaEQdFwXlRXVfpamZq4H1lCEed1RvUv0N2O5oXizWbJPagWADCPm8bCWmfnqDtxF8SSMZMY1RtgtLlklK6RRjak_q4oIHciLVRtk_vU01_vWCkBV5g23T1yOaH0TLq6gfmyYRncIiVZgUeWkztoM4J96fcMYgA";

            var statement = new Statement(jsonStmt);
            var attachment = statement.GetAttachmentByHash("0965225653b117b17ecd7556eda9ab2c5ea627c393bb0fb877d43b41f533d493");
            attachment.SetPayload(base64UrlEncodedString);
            var validator = new StatementValidator();
            var results = validator.Validate(statement);

            results.IsValid.ShouldBeFalse();
        }
    }
}
