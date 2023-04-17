using Email.OTP.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Email.OTP.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class CustomProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomProblemDetails() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="errors"></param>
        public CustomProblemDetails(List<InvalidParameter> errors)
        {
            InvalidParams = errors;
        }
        /// <summary>
        /// Trace Id
        /// </summary>
        [JsonPropertyName("trace-id")]
        public string TraceId { get; set; }

        /// <summary>
        /// Invalid Params
        /// </summary>
        [JsonPropertyName("invalid-param")]
        public List<InvalidParameter> InvalidParams { get; set; } = new List<InvalidParameter>();
    }
}
