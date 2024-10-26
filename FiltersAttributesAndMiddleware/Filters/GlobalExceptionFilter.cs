using Amazon.CognitoIdentityProvider.Model;
using FiltersAttributesAndMiddleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace FiltersAttributesAndMiddleware.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _hostEnviroment;

        /// <summary>
        ///     ctor
        /// </summary>
        public GlobalExceptionFilter(ILogger<IExceptionFilter> logger, IHostEnvironment hostEnviroment)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(DuplicateElementException), HandleCustomExceptions },
                { typeof(ElementNotFoundException), HandleCustomExceptions },
                { typeof(IdentityException), HandleCustomExceptions },
                { typeof(InvalidCredentialsException), HandleCustomExceptions },
                { typeof(AlreadyTakenException), HandleCustomExceptions },
                { typeof(UsernameExistsException), HandleAmazonCognitoExceptions },
                { typeof(InvalidPasswordException), HandleAmazonCognitoExceptions },
                { typeof(CodeMismatchException), HandleAmazonCognitoExceptions },
                { typeof(NotAuthorizedException), HandleAmazonCognitoExceptions },
                { typeof(UserNotFoundException), HandleAmazonCognitoExceptions }
            };
            _logger = logger;
            _hostEnviroment = hostEnviroment;
        }

        private void HandleCustomExceptions(ExceptionContext obj)
        {
            obj.Result = new JsonResult(obj.Exception.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            obj.ExceptionHandled = true;
        }

        private void HandleAmazonCognitoExceptions(ExceptionContext obj)
        {
            var exceptionType = obj.Exception.GetType();

            if (exceptionType == typeof(UsernameExistsException))
            {
                obj.Result = new JsonResult("El correo ya existe")
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }
            else if (exceptionType == typeof(InvalidPasswordException))
            {
                obj.Result = new JsonResult("La contraseña debe tener al menos 8 caracteres, contener al menos 1 número, una letra mayúscula y una letra minúscula")
                {
                    StatusCode = StatusCodes.Status415UnsupportedMediaType
                }; ;
            }
            else if (exceptionType == typeof(CodeMismatchException))
            {
                obj.Result = new JsonResult("El código de verificación proporcionado es inválido, por favor intenta de nuevo.")
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (exceptionType == typeof(NotAuthorizedException))
            {
                obj.Result = new JsonResult("El usuario o contraseña es invalido.")
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            else if (exceptionType == typeof(UserNotFoundException))
            {
                obj.Result = new JsonResult( "Usuario invalido.")
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            obj.ExceptionHandled = true;
        }


        public void OnException(ExceptionContext context)
        {
            HandleException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Error");
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            HandleUnknownException(context);
            return;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ProblemDetails details = new ()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
            //if (!_hostEnviroment.IsDevelopment()) Se debe revisar despues como cambiar el env var
            if (false)
            {
                context.Result = new JsonResult("An error occurred while processing your request.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                context.Result = new JsonResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }


            context.ExceptionHandled = true;
        }
    }
    
}
