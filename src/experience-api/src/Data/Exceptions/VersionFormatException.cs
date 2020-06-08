using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class VersionFormatException : ExperienceDataException
    {
        public VersionFormatException(string message) : base(message)
        {
        }

        public VersionFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
