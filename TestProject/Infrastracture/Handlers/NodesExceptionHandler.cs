using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TestProject.Db.Entity;
using TestProject.Infrastracture.Exceptions;
using TestProject.Services.Abstractions;

namespace TestProject.Infrastracture.Handlers
{
    public class NodesExceptionHandler : IExceptionHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NodesExceptionHandler> _logger;
        private readonly IExceptionRecordConverter _exceptionRecordConverter;

        public NodesExceptionHandler(
            ILogger<NodesExceptionHandler> logger,
            IServiceProvider serviceProvider,
            IExceptionRecordConverter exceptionRecordConverter)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _exceptionRecordConverter = exceptionRecordConverter;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            string type = "Exception";
            if (exception is SecureException || exception.GetType().IsSubclassOf(typeof(SecureException)))
            {
                type = "Secure";
            }

            ExceptionRecord? record = null;

            using (var scope = _serviceProvider.CreateScope())
            {
                var journalService = scope.ServiceProvider.GetRequiredService<IExceptionJournalService>();

                record = await journalService.AddAsync(new ExceptionRecord
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Data = new ExceptionData
                    {
                        Message = exception.Message
                    }
                });
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (record != null)
            {
                await httpContext.Response
                    .WriteAsJsonAsync(_exceptionRecordConverter.ConvertEntityToModel(record), cancellationToken);
            }

            return false;
        }
    }
}
