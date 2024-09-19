namespace Warehouse.Api.Models.ResponseDtos
{
    public class ExceptionResponseDto
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> ValidationMessages { get; set; }

        public int StatusCode { get; set; }
        public ExceptionResponseDto(int statudCode, string type, string message)
        {
            StatusCode = statudCode;
            Type = type;
            Message = message;
        }

        public ExceptionResponseDto(int statudCode, string type, IEnumerable<string> validationMessages)
        {
            StatusCode = statudCode;
            Type = type;
            ValidationMessages = validationMessages;
        }
    }
}
