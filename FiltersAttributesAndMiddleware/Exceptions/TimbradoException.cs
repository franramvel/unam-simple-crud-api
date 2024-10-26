using System.Runtime.Serialization;

namespace FiltersAttributesAndMiddleware.Exceptions
{
    public class BalanceadorException : Exception
    {
        public BalanceadorException(string message) : base(message) { }
        public BalanceadorException()
        {
        }

        public BalanceadorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BalanceadorException(SerializationInfo info, StreamingContext context) : base(info, context)
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
