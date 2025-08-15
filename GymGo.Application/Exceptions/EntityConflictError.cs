using GymGo.Application.Resources;

namespace GymGo.Application.Exceptions;

public sealed class EntityConflictError
    : DomainError
{
    public EntityConflictError(object identifier, string detail)
    {
        Identifier = identifier;
        Message = Strings.ElementoConflicto;
        Detail = detail;
    }

    public object Identifier { get; }
}