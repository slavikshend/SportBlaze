using Microsoft.EntityFrameworkCore;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly SportsShopDbContext _context;

        public UserRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetRegisteredUser(string email)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Role.Name != "UnregisteredUser");
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
