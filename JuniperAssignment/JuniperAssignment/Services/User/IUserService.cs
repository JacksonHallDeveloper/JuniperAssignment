using JuniperAssignment.Models.User;

namespace JuniperAssignment.Services.User
{
    public interface IUserService
    {
        Models.User.User GetCurrentUser();
        void SetCurrentAddress(Address address);
    }
}