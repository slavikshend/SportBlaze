using System.Threading.Tasks;
using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface IFavouritesService
    {
        Task AddToFavourites(int productId, string userEmail);
        Task DeleteFromFavourites(int productId, string userEmail);

        Task<IEnumerable<SimplifiedProductModel>> GetFavouriteProducts(string userEmail);
    }
}
