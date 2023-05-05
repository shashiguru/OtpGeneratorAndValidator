using Email.OTP.Application;
using Email.OTP.Application.Exceptions;
using Email.OTP.Application.Interfaces;
using Email.OTP.Application.Models;
using Email.OTP.Application.Models.AppSettings;
using Email.OTP.Application.Services;
using Email.OTP.Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Xunit;

namespace Email.OTP.UnitTest.Application
{
    public class EmailOperationsTest
    {
        private readonly IEmailService _emailService;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IOptions<EmailConfig> _config;
        private readonly EmailOperations _emailOperations;
        
        public EmailOperationsTest()
        {
            _emailService = A.Fake<IEmailService>();
            _userDetailRepository = A.Fake<IUserDetailRepository>();
            _config = A.Fake<IOptions<EmailConfig>>();
            _emailOperations = new EmailOperations(_emailService, _userDetailRepository, _config);
        }

        [Fact]
        public async Task GenerateOtp_Positive_WhenEmailIsEnteredAndUserExistInDb()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.UpdatedDateTime = actualUserData.UpdatedDateTime.AddSeconds(-100);
            var actualEmailData = CreateEmailData(actualUserData.Email, actualUserData.Otp);

            //Act
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(actualUserData);
            A.CallTo(() => _userDetailRepository.UpdateDetail(actualUserData)).Returns(true);
            A.CallTo(() => _emailService.SendEmail(A<EmailData>.Ignored)).Returns(true);
            var result = await _emailOperations.GenerateOtp(actualUserData.Email);

            //Assert
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userDetailRepository.UpdateDetail(actualUserData)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailService.SendEmail(A<EmailData>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GenerateOtp_Positive_WhenEmailIsEnteredAndUserNotExistInDb()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            UserDetail mockUserData = null;
            actualUserData.UpdatedDateTime = actualUserData.UpdatedDateTime.AddSeconds(-100);
            var actualEmailData = CreateEmailData(actualUserData.Email, actualUserData.Otp);

            //Act
            A.CallTo(() =>  _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(mockUserData);
            A.CallTo(() => _userDetailRepository.CreateDetail(A<UserDetail>.Ignored)).Returns(true);
            A.CallTo(() => _emailService.SendEmail(A<EmailData>.Ignored)).Returns(true);
            var result = await _emailOperations.GenerateOtp(actualUserData.Email);

            //Assert
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userDetailRepository.CreateDetail(A<UserDetail>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailService.SendEmail(A<EmailData>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GenerateOtp_Negative_WhenEmailIsEnteredButDisabledAsOtpGenerated()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.UpdatedDateTime = actualUserData.UpdatedDateTime.AddSeconds(100);

            try
            {
                //Act
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(actualUserData);
                Action act = async () => await _emailOperations.GenerateOtp(actualUserData.Email);
                DisabledException exception = Assert.Throws<DisabledException>(act);
            }
            catch (DisabledException exception)
            {
                //Assert
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
                Assert.Equal("Otp already generated, please try after 1 minute", exception.Message);
                throw;
            }
        }

        [Fact]
        public async Task ValidateOTP_Positive_WhenCorrectOtpEntered()
        {
            //Arrange
            var actualUserData = CreateUserDetail();

            //Act
            A.CallTo(() =>  _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(actualUserData);
            var result = await _emailOperations.ValidateOTP(actualUserData.Email, actualUserData.Otp);

            //Assert
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateOTP_Negative_WhenOtpTryCountMorethan10()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.TryCount = 11;
            var otp = 123456;

            //Act
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(actualUserData);
            var result = await _emailOperations.ValidateOTP(actualUserData.Email, otp);

            //Assert
            A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateOTP_Negative_WhenOtpIsTimeOut()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            actualUserData.UpdatedDateTime = actualUserData.UpdatedDateTime.AddSeconds(-100);

            try
            {
                //Act
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).Returns(actualUserData);
                await _emailOperations.ValidateOTP(actualUserData.Email, actualUserData.Otp);
            }
            catch (TimeOutException exception)
            {
                //Assert
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(actualUserData.Email)).MustHaveHappenedOnceExactly();
                Assert.Equal("Timeout for entered Otp", exception.Message);
            }
        }

        [Fact]
        public async Task ValidateOTP_Negative_WhenEmailNotFound()
        {
            //Arrange
            var actualUserData = CreateUserDetail();
            UserDetail mockUserData = null;
            actualUserData.UpdatedDateTime = DateTime.UtcNow.AddSeconds(100);

            try
            {
                //Act
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(String.Empty)).Returns(mockUserData);
                await _emailOperations.ValidateOTP(String.Empty, actualUserData.Otp);
            }
            catch (NotFoundException exception)
            {
                //Assert
                A.CallTo(() => _userDetailRepository.GetDetailByEmailId(String.Empty)).MustHaveHappenedOnceExactly();
                Assert.Equal("Email not found", exception.Message);
            }
        }

        /// <summary>
        /// CreateUserDetail
        /// </summary>
        /// <returns></returns>
        private UserDetail CreateUserDetail()
        {
            return new UserDetail()
            {
                Email = "shashiguru.keluth@dso.org.sg",
                CreatedDateTime = DateTime.Now.ToSgt(),
                UpdatedDateTime = DateTime.Now.ToSgt(),
                Id = new Guid(),
                Otp = new Random().Next(0, 1000000),
                TryCount = 0
            };
        }

        /// <summary>
        /// CreateEmailData
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        private EmailData CreateEmailData(string email, int? otp)
        {
            return new EmailData()
            {
                FromEmail = _config.Value.FromEmail,
                ToEmail = email,
                Content = $"Your OTP Code is {otp}. The code is valid for 1 minute"
            };
        }
    }
}
