using FluentResults;

namespace GymGo.Application.Exceptions;

public static class ErrorExtensions
{
    private static readonly Type DomainErrorType = typeof(DomainError);

    public static bool IsDomainError(this IError error) => error.GetType().IsDomainError();
    public static bool IsDomainError(this Type type) => DomainErrorType.IsAssignableFrom(type);

    public static DomainError ToDomainError(this IError error) => error as DomainError;

    public static IEnumerable<DomainError> ToDomainErrors(this IEnumerable<IError> errors)
        => errors.Where(e => e.IsDomainError()).Cast<DomainError>();
}