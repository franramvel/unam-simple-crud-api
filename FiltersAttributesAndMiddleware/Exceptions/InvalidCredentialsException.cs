using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
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
                    return "Las credenciales son invalidas";
                }
            }
        }
    }
}
