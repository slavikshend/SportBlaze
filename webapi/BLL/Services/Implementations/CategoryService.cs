using AutoMapper;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Implementations
{
    public class CategoryService : ICRUDService<CategoryModel>
    {
        private readonly ICRUDRepo<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICRUDRepo<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                var createdCategory = await _categoryRepo.CreateAsync(category);
                return _mapper.Map<CategoryModel>(createdCategory);
            }
            catch (Exception ex)
            {
                return null;
            }
        }   

        public async Task<CategoryModel> UpdateAsync(CategoryModel entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                var updatedCategory = await _categoryRepo.UpdateAsync(category);
                return _mapper.Map<CategoryModel>(updatedCategory);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _categoryRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}