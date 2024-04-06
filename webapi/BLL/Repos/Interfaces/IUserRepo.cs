using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IUserRepo
    {
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByLoginAndPassword(LoginModel model);
        public Task<User> UpdateUser(User user);
        public Task<User> AddUser(LoginModel model);
    }
}