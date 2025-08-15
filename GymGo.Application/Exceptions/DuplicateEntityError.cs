using GymGo.Application.Resources;

namespace GymGo.Application.Exceptions;

public sealed class DuplicateEntityError
    : DomainError
{
    public DuplicateEntityError(object identifier)
    {
        Identifier = identifier;
        Message = Strings.ElementoDuplicado;
        Detail = string.Format(Strings.YaExisteElementoX, identifier);
    }

    public object Identifier { get; }
}