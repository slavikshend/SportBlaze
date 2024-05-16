using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface ICategoryService : ICRUDService<CategoryModel>
    {
        Task<IEnumerable<CategoryModel1>> GetAllCategoriesAsync();
    }
}
