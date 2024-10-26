using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message) { }
        public ElementNotFoundException()
        {
        }

        public ElementNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message
        {
            get { if(!base.Message.Contains("was thrown"))
                {
                    return base.Message;
                }
                else
                {
                    return "El elemento solicitado no existe en la base de datos";
                }
            }
        } 
    }
}
