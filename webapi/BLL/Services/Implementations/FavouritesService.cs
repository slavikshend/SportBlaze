using AutoMapper;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Services.Implementations
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IFavouritesRepo _favouritesRepo;

        private readonly IMapper _mapper;

        public FavouritesService(IFavouritesRepo favouritesRepo, IMapper mapper)
        {
            _favouritesRepo = favouritesRepo;
            _mapper = mapper;
        }

        public async Task AddToFavourites(int productId, string userEmail)
        {
            await _favouritesRepo.AddToFavourites(productId, userEmail);
        }

        public async Task DeleteFromFavourites(int productId, string userEmail)
        {
            await _favouritesRepo.DeleteFromFavourites(productId, userEmail);
        }

        public async Task<IEnumerable<SimplifiedProductModel>> GetFavouriteProducts(string userEmail)
        {
            var favouriteProducts = await _favouritesRepo.GetFavouriteProductsAsync(userEmail);
            var simplifiedProducts = _mapper.Map<IEnumerable<SimplifiedProductModel>>(favouriteProducts);

            foreach (var product in simplifiedProducts)
            {
                product.IsFavourite = true; // Since these are favourite products, set IsFavourite to true
            }

            return simplifiedProducts;
        }
    }
}
