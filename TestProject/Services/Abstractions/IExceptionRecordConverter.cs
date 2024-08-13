using TestProject.Db.Entity;
using TestProject.Models;

namespace TestProject.Services.Abstractions
{
    public interface IExceptionRecordConverter
    {
        ExceptionRecordModel ConvertEntityToModel(ExceptionRecord exceptionRecord);
    }
}
