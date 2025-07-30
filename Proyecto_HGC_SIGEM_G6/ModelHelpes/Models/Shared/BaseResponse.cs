using System.ComponentModel.DataAnnotations;

namespace ModelHelper.Models
{
    public class BaseResponse
    {
        public bool hasErrors { get; set; }

        public string errorMessage { get; set; }

    }

    public class BaseResponse<T>
    {
        public bool hasErrors { get; set; }

        public string errorMessage { get; set; }

        public T data { get; set; }

        public List<T> entities { get; set; }

    }
}
