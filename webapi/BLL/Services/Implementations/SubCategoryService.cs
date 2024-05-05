using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Implementations
{
    public class SubCategoryService : ICRUDService<SubCategoryModel>
    {
        private readonly ICRUDRepo<SubCategory> _subCategoryRepo;
        private readonly IMapper _mapper;

        public SubCategoryService(ICRUDRepo<SubCategory> subCategoryRepo, IMapper mapper)
        {
            _subCategoryRepo = subCategoryRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubCategoryModel>> GetAllAsync()
        {
            var subCategories = await _subCategoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SubCategoryModel>>(subCategories);
        }

        public async Task<SubCategoryModel> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryRepo.GetByIdAsync(id);
            return _mapper.Map<SubCategoryModel>(subCategory);
        }

        public async Task<SubCategoryModel> CreateAsync(SubCategoryModel entity)
        {
            var subCategory = new SubCategory
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                CategoryId = entity.CategoryId
            };
            var createdSubCategory = await _subCategoryRepo.CreateAsync(subCategory);
            entity.Id = createdSubCategory.Id;
            entity.CategoryName = createdSubCategory.Category?.Name;
            entity.CategoryImageUrl = createdSubCategory.Category?.Image;
            return entity;
        }

        public async Task<SubCategoryModel> UpdateAsync(SubCategoryModel entity)
        {
            try
            {
                var subCategory = _mapper.Map<SubCategory>(entity);
                var updatedSubCategory = await _subCategoryRepo.UpdateAsync(subCategory);
                return _mapper.Map<SubCategoryModel>(updatedSubCategory);
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
                return await _subCategoryRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
