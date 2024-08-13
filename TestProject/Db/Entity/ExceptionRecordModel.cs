namespace TestProject.Db.Entity
{
    public class ExceptionRecord
    {
        public required string Type { get; set; }
        public required Guid Id { get; set; }
        public ExceptionData? Data { get; set; }
    }

    public class ExceptionData
    {
        public string? Message { get; set; }
    }
}
