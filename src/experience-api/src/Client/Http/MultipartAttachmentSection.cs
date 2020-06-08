using Doctrina.ExperienceApi.Data.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Client.Http
{
    public class MultipartAttachmentSection
    {
        private readonly MultipartSection section;
        public string XExperienceApiHash => GetExperienceApiHash();
        public string ContentTransferEncoding => GetContentTransferEncoding();

        private string GetContentTransferEncoding()
        {
            if (section.Headers.TryGetValue(ApiHeaders.ContentTransferEncoding, out StringValues cteValues))
            {
                return cteValues;
            }
            return null;
        }

        private string GetExperienceApiHash()
        {
            if (section.Headers.TryGetValue(ApiHeaders.XExperienceApiHash, out StringValues hashValues))
            {
                return hashValues;
            }
            return null;
        }

        public MultipartAttachmentSection(MultipartSection section)
        {
            this.section = section;


            if (!string.IsNullOrEmpty(ContentTransferEncoding))
            {
                if (ContentTransferEncoding != "binary")
                {
                    throw new MultipartSectionException($"MUST include a {ApiHeaders.ContentTransferEncoding} parameter with a value of binary in each part's header after the first (Statements) part.");
                }
            }
            else
            {
                throw new MultipartSectionException($"{ApiHeaders.ContentTransferEncoding}'' header is missing.");
            }

            if (string.IsNullOrEmpty(XExperienceApiHash))
            {
                // MUST include a Content-Transfer-Encoding parameter with a value of binary in each part's header after the first (Statements) part.
                throw new MultipartSectionException($"'{ApiHeaders.XExperienceApiHash}' is missing.");
            }
        }

        public async Task<byte[]> ReadAsByteArrayAsync()
        {
            using(StreamReader sr = new StreamReader(section.Body))
            {
                return Encoding.UTF8.GetBytes(await sr.ReadToEndAsync());
            }
        }
    }
}