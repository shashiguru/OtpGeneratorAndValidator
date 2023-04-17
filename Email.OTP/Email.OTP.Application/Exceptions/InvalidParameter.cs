using System.Text.Json.Serialization;

namespace Email.OTP.Application.Exceptions
{
    /// <summary>
    /// InvalidParameter
    /// </summary>
    public class InvalidParameter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
