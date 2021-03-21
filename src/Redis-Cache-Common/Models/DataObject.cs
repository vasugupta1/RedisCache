using System.Text.Json.Serialization;

namespace Redis_Cache_Common.Models
{
    public class DataObject
    {
        [JsonPropertyName("Guid")]public string Guid {get;set;}
        [JsonPropertyName("Value")]public string Value {get;set;}
        [JsonPropertyName("Email")]public string Email {get;set;}       
    }
}