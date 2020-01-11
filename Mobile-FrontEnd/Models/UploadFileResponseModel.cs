using System.Text.Json.Serialization;

namespace Mobile_FrontEnd.Models
{
    public class UploadFileResponseModel
    {
        [JsonPropertyName("msg")]
        public string msg { get; set; }
        
        [JsonPropertyName("fileUrl")]
        public string fileUrl { get; set; }
    }
}