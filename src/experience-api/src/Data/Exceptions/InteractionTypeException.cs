using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class InteractionTypeException : ExperienceDataException
    {
        public InteractionTypeException(string type)
            : base($"'{type}' is not a valid InteractionType.")
        {
        }
    }
}
