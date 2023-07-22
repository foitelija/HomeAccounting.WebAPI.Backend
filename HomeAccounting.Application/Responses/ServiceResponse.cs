namespace HomeAccounting.Application.Responses
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } 
        public string? ErrorMessage { get; set; }
    }
}
