﻿using Microsoft.EntityFrameworkCore;
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
                .Include(u => u.Role).Include(u => u.Favourites)
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
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                if (existingUser.RoleId == 3)
                {
                    existingUser.RoleId = 2;
                    _context.Users.Update(existingUser);
                    await _context.SaveChangesAsync();
                    return existingUser;
                }
                else return null;
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
        }
       public async Task<IEnumerable<User>> GetAllRegisteredUsers()
{
    var registeredUsers = await _context.Users.AsNoTracking()
        .Include(u => u.Role)
        .Include(u => u.Favourites)
        .ThenInclude(p => p.Product)
        .Where(u => u.Role.Name != "UnregisteredUser")
        .ToListAsync();

    return registeredUsers;
}


    }
}
