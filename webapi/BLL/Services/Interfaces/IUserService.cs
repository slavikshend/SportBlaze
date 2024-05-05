using System.Threading.Tasks;
using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginModel loginModel);
        Task<bool> RegisterAsync(RegisterModel registerModel);
        Task<bool> UpdateUserAsync(UserModel userModel, string email);
        Task<UserModel> GetAsync(string email);
        Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword);
    }
}