using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class MboxFormatException : ExperienceDataException
    {
        public MboxFormatException(string message) : base(message)
        {
        }

        public MboxFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
