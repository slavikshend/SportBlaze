using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.MN;

namespace webapi.BLL.Repos.Implementations
{
    public class FavouritesRepo : IFavouritesRepo
    {
        private readonly SportsShopDbContext _context;

        public FavouritesRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task AddToFavourites(int productId, string userEmail)
        {
            var favourite = new Favourites
            {
                ProductId = productId,
                UserEmail = userEmail
            };

            _context.Favourites.Add(favourite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromFavourites(int productId, string userEmail)
        {
            var favourite = await _context.Favourites.FirstOrDefaultAsync(f => f.ProductId == productId && f.UserEmail == userEmail);
            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
