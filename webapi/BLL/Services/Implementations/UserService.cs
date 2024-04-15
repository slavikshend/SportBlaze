using System.Threading.Tasks;
using webapi.BLL.Helpers;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly JwtCreator _jwtCreator;
        private readonly PasswordHasher _passwordHasher;

        public UserService(IUserRepo userRepo, JwtSettings jwtSettings)
        {
            _userRepo = userRepo;
            _jwtCreator = new JwtCreator(jwtSettings);
            _passwordHasher = new PasswordHasher();
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var user = await _userRepo.GetRegisteredUser(loginModel.Email);
            if (user == null || !_passwordHasher.VerifyPassword(loginModel.Password, user.HashedPassword, user.Salt))
            {
                return null;
            }

            var userRole = user.Role?.Name;
            return _jwtCreator.GenerateJwtToken(user.Email, userRole);
        }

        public async Task<bool> UpdateUserAsync(UserModel userModel, string email)
        {
            var user = await _userRepo.GetRegisteredUser(email);
            if (user == null)
            {
                return false;
            }

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Phone = userModel.Phone;
            user.City = userModel.City;
            user.Address = userModel.Address;

            if (!string.IsNullOrEmpty(userModel.Password))
            {
                var (hashedPassword, salt) = _passwordHasher.HashPassword(userModel.Password);

                user.HashedPassword = hashedPassword;
                user.Salt = salt;
            }

            var result = await _userRepo.UpdateUser(user);
            return result != null;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            var existingUser = await _userRepo.GetRegisteredUser(registerModel.Email);
            if (existingUser != null)
            {
                return false;
            }

            var (hashedPassword, salt) = _passwordHasher.HashPassword(registerModel.Password);

            var user = new User
            {
                Email = registerModel.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                RoleId = 2 
            };

            await _userRepo.AddUser(user);
            return true;
        }
    }
}
