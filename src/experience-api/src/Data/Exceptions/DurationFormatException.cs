using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class DurationFormatException : ExperienceDataException
    {
        public DurationFormatException(string message) : base(message)
        {
        }

        public DurationFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
