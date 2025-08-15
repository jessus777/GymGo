using GymGo.Application.Contracts.Infrastructure;
using GymGo.Domain.Domain_Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymGo.Application.Events.Handlers
{
    public class ClientCreatedDomainEventHandler
        : INotificationHandler<ClientCreatedDomainEvent>

    {
        private readonly ILogger<ClientCreatedDomainEventHandler> _logger;
        private readonly IFileLoggerService _fileLoggerService;
        //private readonly IWhatsAppService _whatsAppService;
        //private readonly IEmailService _emailService;

        public ClientCreatedDomainEventHandler(ILogger<ClientCreatedDomainEventHandler> logger, IFileLoggerService fileLoggerService)
        {
            _logger = logger;
            _fileLoggerService = fileLoggerService;
        }

        //public ClientCreatedDomainEventHandler(ILogger<ClientCreatedDomainEventHandler> logger, IWhatsAppService whatsAppService, IEmailService emailService)
        //{
        //    _logger = logger;
        //    _whatsAppService = whatsAppService;
        //    _emailService = emailService;
        //}

        public async Task Handle(ClientCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var client = notification.Client;

            // Enviar email de bienvenida
            //await _emailService.SendAsync(
            //    client.Email,
            //    "¡Bienvenido a GymGo!",
            //    $"Hola {client.Name}, tu cuenta ha sido creada exitosamente."
            //);

            //// Enviar mensaje de WhatsApp (si el número está disponible)
            //if (!string.IsNullOrWhiteSpace(client.PhoneNumber))
            //{
            //    await _whatsAppService.SendMessageAsync(
            //        client.PhoneNumber,
            //        $"¡Hola {client.Name}! Bienvenido a GymGo. Tu cuenta ya está activa."
            //    );
            //}
            _logger.LogInformation("Notificaciones enviadas para el cliente {ClientId}", client.Id);
            _fileLoggerService.LogInformation(
                $"Notificaciones enviadas para el cliente {client.Id} - {client.Name} - {client.Email}"
            );
        }
    }
}
