using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<LoginModel> LoginAsync(LoginModel loginModel);
        public Task<RegisterModel> RegisterAsync(RegisterModel registerModel);

        public Task<UserModel> UpdateUserAsync(int id);
    }
}
