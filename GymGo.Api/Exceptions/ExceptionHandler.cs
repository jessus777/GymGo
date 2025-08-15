using FluentValidation;
using GymGo.Api.Extensions;
using GymGo.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace GymGo.Api.Exceptions
{
    public sealed class ExceptionHandler
        : IExceptionHandler
    {
        private readonly IHostEnvironment _environment;

        public ExceptionHandler(IHostEnvironment environment)
        {
            _environment = environment;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var culture = httpContext.GetPreferredCulture();
            var status = exception switch
            {
                AuthenticationException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                ExternalServiceException => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError
            };
            var title = exception switch
            {
                AuthenticationException => "Error de autenticacion.",//Strings.ErrorAutenticacion,
                ValidationException => "Error de validacion.", //Strings.ErrorValidacion,
                ExternalServiceException => "Error de servicio externo.", //Strings.ErrorServicioExterno,
                _ => null
            };

            // Incluir detalles de excepciones en ambientes NO productivos
            var includeDetails = !_environment.IsProduction();

            var activity = System.Diagnostics.Activity.Current;
            if (exception is ValidationException validationException)
            {
                // Agrupa los errores por propiedad (para ValidationProblemDetails)
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                // Extrae solo los mensajes de error en una lista de strings
                var errorMessages = validationException.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var validationProblemDetails = new ValidationProblemDetails(errors)
                {
                    Status = status,
                    Type = $"https://developer.mozilla.org/{culture}/docs/Web/HTTP/Status/{status}",
                    Title = includeDetails ? "Error de validación" : "Error de validación",
                    Detail = includeDetails ? exception.Message : null,
                    Instance = httpContext.Request.Path
                };

                validationProblemDetails.Extensions.Add("traceId", activity?.Id ?? httpContext.TraceIdentifier);
                validationProblemDetails.Extensions.Add("errorMessages", errorMessages);

                if (includeDetails)
                    validationProblemDetails.Extensions.Add("stackTrace", exception.ToString());

                httpContext.Response.StatusCode = status;
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);
                return true;
            }
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Type = $"https://developer.mozilla.org/{culture}/docs/Web/HTTP/Status/{status}",
                Title = includeDetails ? title ?? exception.GetType().Name : "Error de servidor interno.", //Strings.ErrorInternoServidor,
                Detail = includeDetails ? exception.Message : null,
                Extensions =
            {
                {"traceId", activity?.Id ?? httpContext.TraceIdentifier}
            }
            };

            //if (exception is ValidationException validationException)
            //    problemDetails.Extensions.Add("errors", validationException.Errors);

            if (includeDetails)
                problemDetails.Extensions.Add("stackTrace", exception.ToString());

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
