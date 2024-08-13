using TestProject.Db;
using TestProject.Db.Entity;
using TestProject.Services.Abstractions;

namespace TestProject.Services
{
    public class ExceptionJournalService : IExceptionJournalService
    {
        private readonly NodesDbContext _context;
        private readonly ILogger<ExceptionJournalService> _logger;

        public ExceptionJournalService(
            NodesDbContext context, ILogger<ExceptionJournalService> log)
        {
            _context = context;
            _logger = log;
        }

        public async Task<ExceptionRecord?> AddAsync(ExceptionRecord exceptionRecord)
        {
            if (exceptionRecord == null)
            {
                throw new ArgumentNullException(nameof(exceptionRecord));
            }

            _context.ExceptionJournal.Add(exceptionRecord);
            await _context.SaveChangesAsync();

            return exceptionRecord;
        }
    }
}
