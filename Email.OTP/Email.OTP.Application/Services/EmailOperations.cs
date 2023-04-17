using Email.OTP.Application.Exceptions;
using Email.OTP.Application.Interfaces;
using Email.OTP.Application.Models;
using Email.OTP.Application.Models.AppSettings;
using Email.OTP.Domain.Models;
using Email.OTP.WebAPI.Models.Response;
using Microsoft.Extensions.Options;

namespace Email.OTP.Application.Services
{
    public class EmailOperations : IEmailOpertions
    {
        private readonly IEmailService _emailService;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IOptions<EmailConfig> _config;
        public EmailOperations(IEmailService emailService, IUserDetailRepository userDetailRepository, IOptions<EmailConfig> config)
        {
            _emailService = emailService;
            _userDetailRepository = userDetailRepository;
            _config = config;
        }

        /// <summary>
        /// GenerateOtp
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="DisabledException"></exception>
        public async Task<GenerateResponseDto> GenerateOtp(string email)
        {
            var UserDetail = new UserDetail()
            {
                Email = email,
                CreatedDateTime = DateTime.Now.ToSgt(),
                UpdatedDateTime = DateTime.Now.ToSgt(),
                Id = new Guid(),
                Otp = CreateRandomOtp(),
                TryCount = 0
            };

            var userDetailData = await _userDetailRepository.GetDetailByEmailId(email);
            if (userDetailData != null)
            {
                if (userDetailData.UpdatedDateTime.AddSeconds(60) > DateTime.Now.ToSgt())
                {
                    throw new DisabledException("Otp already generated, please try after 1 minute");
                    //otp already generated
                }
                userDetailData.Otp = CreateRandomOtp();
                await _userDetailRepository.UpdateDetail(userDetailData);
                await SendEmail(email, userDetailData.Otp);
            }
            else
            {
                await _userDetailRepository.CreateDetail(UserDetail);
                await SendEmail(email, UserDetail.Otp);
            }
            return CreateGenerateResponse(email, UserDetail.Otp);
        }

        /// <summary>
        /// ValidateOTP
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        /// <exception cref="TimeOutException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public async Task<bool> ValidateOTP(string email, int? otp)
        {
            var userDetail = await _userDetailRepository.GetDetailByEmailId(email);
            if (userDetail != null)
            {
                if (userDetail.TryCount <= 10)
                {
                    //timeout check
                    if (userDetail.UpdatedDateTime.AddSeconds(60) > DateTime.Now.ToSgt())
                    {
                        if (userDetail.Otp != otp)
                        {
                            userDetail.TryCount++;
                            await _userDetailRepository.UpdateDetail(userDetail);
                            //Otp invalid
                            return false;
                        }
                        return true;
                    }
                    throw new TimeOutException("Timeout for entered Otp");
                }
                return false;
            }
            throw new NotFoundException("Email not found");
        }

        /// <summary>
        /// CreateRandomOtp
        /// </summary>
        /// <returns></returns>
        private int CreateRandomOtp()
        {
            var generator = new Random();
            return generator.Next(0, 1000000);
        }

        /// <summary>
        /// CreateGenerateResponse
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns></returns>

        private GenerateResponseDto CreateGenerateResponse(string email, int? otp)
        {
            GenerateResponseDto response = new GenerateResponseDto()
            {
                Email = email,
                Otp = otp
            };
            return response;
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        private async Task<bool> SendEmail(string email, int? otp)
        {
            var emailData = new EmailData()
            {
                FromEmail = _config.Value.FromEmail,
                ToEmail = email,
                Content = $"Your OTP Code is {otp}. The code is valid for 1 minute"
            };
            await _emailService.SendEmail(emailData);
            return true;
        }
    }
}
