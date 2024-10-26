using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(string message) : base(message) { }

        public IdentityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                if (!base.Message.Contains("was thrown"))
                {
                    return base.Message;
                }
                else
                {
                    return "Error en Identity";
                }
            }
        }
    }
}
