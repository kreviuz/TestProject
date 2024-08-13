using AutoMapper;
using TestProject.Db.Entity;
using TestProject.Models;
using TestProject.Services.Abstractions;

namespace TestProject.Services
{
    public class ExceptionRecordConverter : IExceptionRecordConverter
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExceptionRecordConverter> _logger;

        public ExceptionRecordConverter(IMapper mapper, ILogger<ExceptionRecordConverter> log)
        {
            _mapper = mapper;
            _logger = log;
        }

        public ExceptionRecordModel ConvertEntityToModel(ExceptionRecord exceptionRecord)
        {
            ExceptionRecordModel exceptionRecordModel = _mapper.Map<ExceptionRecordModel>(exceptionRecord);
            return exceptionRecordModel;
        }
    }
}
