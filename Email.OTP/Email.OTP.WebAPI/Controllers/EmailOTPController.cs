using Email.OTP.Application.Exceptions;
using Email.OTP.Application.Interfaces;
using Email.OTP.Domain.Models;
using Email.OTP.WebAPI.Helpers;
using Email.OTP.WebAPI.Models;
using Email.OTP.WebAPI.Models.Request;
using Email.OTP.WebAPI.Models.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Email.OTP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class EmailOTPController : ControllerBase
    {
        private readonly IEmailOpertions _emailOpertions;
        public EmailOTPController(IEmailOpertions emailOpertions)
        {
                _emailOpertions = emailOpertions;
        }

        [Route("generate")]
        [HttpPost]
        [SwaggerOperation(Summary = Constants.Generate, Description = Constants.GenerateAnOtpForAnEmail)]
        [SwaggerResponse(200, Type = typeof(GenerateResponseDto))]
        [SwaggerResponse(400, Type = typeof(CustomProblemDetails))]
        [SwaggerResponse(404, Type = typeof(CustomProblemDetails))]
        [SwaggerResponse(500, Type = typeof(CustomProblemDetails))]
        public async Task<IActionResult> Generate([FromBody] GenerateRequestDto generateRequestDto)
        {
            try
            {
                var data = await _emailOpertions.GenerateOtp(generateRequestDto.Email);
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch(DisabledException disabledException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, disabledException.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.InternalServerError);
            }
        }

        [Route("validate")]
        [HttpPut]
        [SwaggerOperation(Summary = Constants.Validate, Description = Constants.ValidateAnOtp)]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400, Type = typeof(CustomProblemDetails))]
        [SwaggerResponse(404, Type = typeof(CustomProblemDetails))]
        [SwaggerResponse(500, Type = typeof(CustomProblemDetails))]
        public async Task<IActionResult> Validate([FromBody] ValidateRequestDto validateRequestDto)
        {
            try
            {
                if (await _emailOpertions.ValidateOTP(validateRequestDto.Email, validateRequestDto.Otp))
                {
                    return StatusCode(StatusCodes.Status200OK, "Validated succesfully.");
                }
                return StatusCode(StatusCodes.Status200OK, "Invalid Otp entered.");
            }
            catch (TimeOutException timeOutException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, timeOutException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, notFoundException.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.InternalServerError);
            }

        }
    }
}
