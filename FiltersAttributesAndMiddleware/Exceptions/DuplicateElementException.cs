using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class DuplicateElementException:Exception
    {
        public DuplicateElementException(string message) : base(message) { }
        public DuplicateElementException()
        {
        }

        public DuplicateElementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicateElementException(SerializationInfo info, StreamingContext context) : base(info, context)
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
                    return "El elemento que intentas creas ya existe en la base de datos.";
                }
            }
        }
    }
}
