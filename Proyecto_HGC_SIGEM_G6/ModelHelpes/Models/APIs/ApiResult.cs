namespace Proyecto_HGC_SIGEM_G6.Services
{
  
    public class ApiResult<T>
    {
        public bool hasErrors { get; set; }
        public string? errorMessage { get; set; }
        public T? data { get; set; }
    }
}
