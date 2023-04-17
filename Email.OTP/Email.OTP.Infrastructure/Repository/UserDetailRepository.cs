using Email.OTP.Application.Interfaces;
using Email.OTP.Domain.Models;
using Email.OTP.Infrastructure.Interfaces;

namespace Email.OTP.Infrastructure.Repository
{
    public class UserDetailRepository: IUserDetailRepository
    {
        private readonly IOtpGenerateDbContext _context;

        public UserDetailRepository(IOtpGenerateDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// CreateDetail
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public async Task<bool> CreateDetail(UserDetail userDetail)
        {
            _context.UserDetail.Add(userDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// GetDetailByEmailId
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<UserDetail> GetDetailByEmailId(string emailId)
        {
            return _context.UserDetail.FirstOrDefault(x=>x.Email==emailId);
        }

        /// <summary>
        /// UpdateDetail
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDetail(UserDetail userDetail)
        {
            _context.UserDetail.Update(userDetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
