using Newtonsoft.Json;

namespace StockRestApi.Auth.Models
{
    public class ErrorModel
    {
        public string StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
