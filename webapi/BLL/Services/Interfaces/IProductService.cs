using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface IProductService : ICRUDService<ProductModel>
    {
        Task<IEnumerable<SimplifiedProductModel>> GetSpecialOfferProductsAsync(string email);
    }
}
