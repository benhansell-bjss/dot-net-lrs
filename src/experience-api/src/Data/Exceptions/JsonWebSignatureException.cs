using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class JsonWebSignatureException : Exception
    {
        public JsonWebSignatureException(string message) : base(message)
        {
        }

        public JsonWebSignatureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
