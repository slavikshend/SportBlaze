using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IFavouritesRepo
    {
        Task AddToFavourites(int productId, string userEmail);
        Task DeleteFromFavourites(int productId, string userEmail);
        Task<IEnumerable<Product>> GetFavouriteProductsAsync(string userEmail);

    }
}
