namespace TestProject.Models
{
    public class ExceptionRecordModel
    {
        public required string Type { get; set; }
        public required string Id { get; set; }
        public ExceptionDataModel? Data { get; set; }
    }

    public class ExceptionDataModel
    {
        public string? Message { get; set; }
    }
}
