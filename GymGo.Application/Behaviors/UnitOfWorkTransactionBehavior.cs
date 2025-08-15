using GymGo.Application.Contracts.Infrastructure;
using GymGo.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymGo.Application.Behaviors
{
    public class UnitOfWorkTransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnitOfWorkTransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IFileLoggerService _fileLoggerService;

        public UnitOfWorkTransactionBehavior(
            IUnitOfWork unitOfWork, 
            ILogger<UnitOfWorkTransactionBehavior<TRequest, TResponse>> logger, 
            IFileLoggerService fileLoggerService
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _fileLoggerService = fileLoggerService;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken
            )
        {
            try
            {
                //await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var response = await next(cancellationToken);
                //await _unitOfWork.CommitTransactionAsync(cancellationToken);
                _fileLoggerService.LogInformation($"Processing request {typeof(TRequest).Name} with response {typeof(TResponse).Name}");
                return response;
            }
            catch (Exception ex)
            {
                _fileLoggerService.LogError($"Error processing request {typeof(TRequest).Name}", ex);
                _logger.LogError(ex, "Error processing request {RequestType}", typeof(TRequest).Name);
                await _unitOfWork.Rollback();
                _unitOfWork.Dispose();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
