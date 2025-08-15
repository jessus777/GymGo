using GymGo.Application.Resources;

namespace GymGo.Application.Exceptions;

public sealed class EntityNotFoundError
    : DomainError
{
    public EntityNotFoundError(object identifier)
    {
        Identifier = identifier;
        Message = Strings.ElementoNoEncontrado;
        Detail = string.Format(Strings.NoSeEncontroElementoConId, identifier);
    }

    public object Identifier { get; }
}