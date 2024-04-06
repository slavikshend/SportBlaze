using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Implementations
{
    public class UserRepo //: IUserRepo
    {
        private readonly SportsShopDbContext context;
        public UserRepo(SportsShopDbContext _context)
        {
            context = _context;
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
