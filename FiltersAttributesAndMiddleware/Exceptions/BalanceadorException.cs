using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class TimbradoException : Exception
    {
        public TimbradoException(string message) : base(message) { }
        public TimbradoException()
        {
        }

        public TimbradoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TimbradoException(SerializationInfo info, StreamingContext context) : base(info, context)
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
                    return "Ha ocurrido un error en el proceso de timbrado.";
                }
            }
        } 
    }
}
