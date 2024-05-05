using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IProductRepo : ICRUDRepo<Product>
    {
        Task<IEnumerable<Product>> GetSpecialOfferProductsAsync(string email);
    }

}
