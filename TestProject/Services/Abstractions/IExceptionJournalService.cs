using TestProject.Db.Entity;

namespace TestProject.Services.Abstractions
{
    public interface IExceptionJournalService
    {
        Task<ExceptionRecord?> AddAsync(ExceptionRecord node);
    }
}
