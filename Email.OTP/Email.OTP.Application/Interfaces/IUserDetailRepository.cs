using Email.OTP.Domain.Models;

namespace Email.OTP.Application.Interfaces
{
    public interface IUserDetailRepository
    {
        Task<bool> CreateDetail(UserDetail userDetail);
        Task<UserDetail> GetDetailByEmailId(string emailId);
        Task<bool> UpdateDetail(UserDetail userDetail);
    }
}
