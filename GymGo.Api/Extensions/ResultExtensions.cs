using FluentResults;
using GymGo.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymGo.Api.Extensions
{
    public static class ResultExtensions
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

        public static IActionResult ToProblemDetails<TResult>(
        this Result<TResult> result,
        int? statusCode = null,
        string? title = null,
        string? detail = null,
        string? type = null
    ) => ToProblemDetails(result.ToResult(), statusCode, title, detail, type);

        public static IActionResult ToProblemDetails(
            this Result result,
            int? statusCode = null,
            string? title = null,
            string? detail = null,
            string? type = null
        )
        {
            if (result.IsSuccess || result.Errors.Count == 0)
                throw new InvalidOperationException("Resultado no fallido.");

            var firstError = result.Errors.FirstOrDefault();

            var finalStatusCode = statusCode;
            if (!statusCode.HasValue && (firstError?.IsDomainError() ?? false))
            {
                // Resolver código de estado a partir del tipo de error de dominio
                finalStatusCode = firstError.ToDomainError() switch
                {
                    EntityInvalidDataError => 400,
                    EntityNotFoundError => 404,
                    EntityConflictError => 409,
                    DuplicateEntityError => 409,
                    _ => 500
                };
            }

            var activity = System.Diagnostics.Activity.Current;

            var problemDetails = new ProblemDetails
            {
                Status = finalStatusCode ?? StatusCodes.Status500InternalServerError,
                Type = type ?? $"https://httpstatuses.com/{finalStatusCode}",
                Title = title ?? firstError?.Message,
                Detail = detail ?? firstError?.ToDomainError()?.Detail,
                Extensions =
            {
                {"traceId", activity?.Id}
            }
            };

            if (result.Errors.Count != 0)
            {
                problemDetails.Extensions.Add(
                    "errors",
                    result.Errors.Select(e => new
                    {
                        e.Message,
                        Reasons = e.Reasons.Select(r => new
                        {
                            r.Message
                        })
                    })
                    );
            }

            return new ContentResult
            {
                StatusCode = problemDetails.Status,
                Content = JsonSerializer.Serialize(problemDetails, JsonSerializerOptions),
                ContentType = "application/problem+json"
            };
        }
    }
}
