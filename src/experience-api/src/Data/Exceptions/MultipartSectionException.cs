using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class MultipartSectionException : ExperienceDataException
    {
        public MultipartSectionException(string message)
            : base(message)
        {
        }
    }
}
