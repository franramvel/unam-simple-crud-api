using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class AlreadyTakenException : Exception
    {
        public AlreadyTakenException(string message) : base(message) { }
        public AlreadyTakenException()
        {
        }

        public AlreadyTakenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyTakenException(SerializationInfo info, StreamingContext context) : base(info, context)
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
                    return "El elemento que intentas tomar ya lo ha tomado otro usuario.";
                }
            }
        }
    }
}
