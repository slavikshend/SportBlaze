using System.Threading.Tasks;

namespace webapi.BLL.Services.Interfaces
{
    public interface IFavouritesService
    {
        Task AddToFavourites(int productId, string userEmail);
        Task DeleteFromFavourites(int productId, string userEmail);
    }
}
