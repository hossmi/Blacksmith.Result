using Blacksmith.Validations.Exceptions;
using System.Runtime.Serialization;

namespace Blacksmith.Exceptions
{
    public class ValueRequestedOnUnsuccessResultException : DomainException
    {
        public ValueRequestedOnUnsuccessResultException() : base()
        {
        }

        protected ValueRequestedOnUnsuccessResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}