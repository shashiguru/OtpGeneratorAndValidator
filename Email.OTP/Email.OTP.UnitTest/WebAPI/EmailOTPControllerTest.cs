using Email.OTP.Application.Exceptions;
using Email.OTP.Application.Interfaces;
using Email.OTP.WebAPI.Controllers;
using Email.OTP.WebAPI.Models.Request;
using Email.OTP.WebAPI.Models.Response;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace Email.OTP.UnitTest.Controller
{
    public class EmailOTPControllerTest
    {
        private readonly IEmailOpertions _emailOpertions;
        private readonly EmailOTPController _emailOTPController;
        public EmailOTPControllerTest()
        {
            _emailOpertions = A.Fake<IEmailOpertions>();
            _emailOTPController = new EmailOTPController(_emailOpertions);
        }

        [Fact]
        public async Task GenerateOtp_Positive_WhenValidEmailEntered()
        {
            //Arrange
            var actual = CreateGenerateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(()=> _emailOpertions.GenerateOtp(actual.Email)).Returns(expected);
            IActionResult result = await _emailOTPController.Generate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, (int)((ObjectResult)result).StatusCode);
            Assert.Equal(expected, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task GenerateOtp_Negative_WhenValidEmailEnteredAndReturnsDisabledWith400()
        {
            //Arrange
            var actual = CreateGenerateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(() => _emailOpertions.GenerateOtp(actual.Email)).Throws<DisabledException>();
            IActionResult result = await _emailOTPController.Generate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GenerateOtp_Negative_WhenValidEmailEnteredAndReturnsInternalServer500()
        {
            //Arrange
            var actual = CreateGenerateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(() => _emailOpertions.GenerateOtp(actual.Email)).Throws<Exception>();
            IActionResult result = await _emailOTPController.Generate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task ValidateOtp_Positive_WhenValidEmailAndOtpEntered()
        {
            //Arrange
            var actual = CreateValidateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(() => _emailOpertions.ValidateOTP(actual.Email, actual.Otp)).Returns(true);
            IActionResult result = await _emailOTPController.Validate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, (int)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task ValidateOtp_Negative_WhenValidEmailAndOtpEnteredReturnTimeOutWith400()
        {
            //Arrange
            var actual = CreateValidateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(() => _emailOpertions.ValidateOTP(actual.Email, actual.Otp)).Throws<TimeOutException>();
            IActionResult result = await _emailOTPController.Validate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task ValidateOtp_Negative_WhenValidEmailAndOtpEnteredReturnNotFoundWith400()
        {
            //Arrange
            var actual = CreateValidateRequestDto();
            var expected = CreateGenerateResponseDto();

            //Act 
            A.CallTo(() => _emailOpertions.ValidateOTP(actual.Email, actual.Otp)).Throws<NotFoundException>();
            IActionResult result = await _emailOTPController.Validate(actual);

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)((ObjectResult)result).StatusCode);
        }

        /// <summary>
        /// CreateGenerateRequestDto
        /// </summary>
        /// <returns></returns>
        private static GenerateRequestDto CreateGenerateRequestDto()
        {
            return new GenerateRequestDto
            {
                Email = "shashiguru.keluth@dso.org.sg"
            };
        }

        /// <summary>
        /// CreateGenerateResponseDto
        /// </summary>
        /// <returns></returns>
        private static GenerateResponseDto CreateGenerateResponseDto()
        {
            return new GenerateResponseDto
            {
                Email = "shashiguru.keluth@dso.org.sg",
                Otp = 123456
            };
        }

        /// <summary>
        /// CreateValidateRequestDto
        /// </summary>
        /// <returns></returns>
        private static ValidateRequestDto CreateValidateRequestDto()
        {
            return new ValidateRequestDto
            {
                Email = "shashiguru.keluth@dso.org.sg",
                Otp = 123456
            };
        }

    }
}
