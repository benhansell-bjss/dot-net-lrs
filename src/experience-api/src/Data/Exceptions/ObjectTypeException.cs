using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class ObjectTypeException : ExperienceDataException
    {
        public ObjectTypeException(string type)
            : base($"'{type}' is not a valid ObjectType.")
        {
        }
    }
}
