using System.Text.Json.Serialization;

namespace CoreApi.WebApp.Models
{
    public class CustomResponseViewModel<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
