using System.Threading.Tasks;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Services.Implementations
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IFavouritesRepo _favouritesRepo;

        public FavouritesService(IFavouritesRepo favouritesRepo)
        {
            _favouritesRepo = favouritesRepo;
        }

        public async Task AddToFavourites(int productId, string userEmail)
        {
            await _favouritesRepo.AddToFavourites(productId, userEmail);
        }

        public async Task DeleteFromFavourites(int productId, string userEmail)
        {
            await _favouritesRepo.DeleteFromFavourites(productId, userEmail);
        }
    }
}
