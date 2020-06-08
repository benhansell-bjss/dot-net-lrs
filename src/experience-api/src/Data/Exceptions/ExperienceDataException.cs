using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class ExperienceDataException : Exception
    {
        public ExperienceDataException() : base() { }

        public ExperienceDataException(string message) : base(message)
        {
        }

        public ExperienceDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
