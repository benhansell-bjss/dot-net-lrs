using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class IriFormatException : ExperienceDataException
    {
        public IriFormatException(string message) : base(message)
        {
        }
    }
}
