using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IUserRepo
    {
        public Task<User> GetRegisteredUser(string email);
        public Task<User> UpdateUser(User user);
        public Task<User> AddUser(User user);
        public Task<IEnumerable<User>> GetAllRegisteredUsers();
    }
}