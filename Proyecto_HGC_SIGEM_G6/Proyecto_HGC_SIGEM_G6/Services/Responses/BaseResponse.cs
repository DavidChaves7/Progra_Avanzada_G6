namespace Proyecto_HGC_SIGEM_G6.Services.Responses
{
    public class ApiResult
    {
        public bool hasErrors { get; set; } = false;
        public string? errorMessage { get; set; }
    }

    public class BaseResponse<T> : ApiResult
    {
        public T? data { get; set; }
    }
}
