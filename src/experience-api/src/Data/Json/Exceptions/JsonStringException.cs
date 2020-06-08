using Doctrina.ExperienceApi.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Data.Json.Exceptions
{
    public class JsonStringException : ExperienceDataException
    {
        public JsonStringException(string message) : base(message)
        {
        }

        public JsonStringException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
