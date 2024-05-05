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
    public class BrandService : ICRUDService<BrandModel>
    {
        private readonly ICRUDRepo<Brand> _brandRepo;
        private readonly IMapper _mapper;

        public BrandService(ICRUDRepo<Brand> brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandModel>> GetAllAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandModel>>(brands);
        }

        public async Task<BrandModel> GetByIdAsync(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            return _mapper.Map<BrandModel>(brand);
        }

        public async Task<BrandModel> CreateAsync(BrandModel entity)
        {
            try
            {
                var brand = _mapper.Map<Brand>(entity);
                var createdBrand = await _brandRepo.CreateAsync(brand);
                return _mapper.Map<BrandModel>(createdBrand);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BrandModel> UpdateAsync(BrandModel entity)
        {
            try
            {
                var brand = _mapper.Map<Brand>(entity);
                var updatedBrand = await _brandRepo.UpdateAsync(brand);
                return _mapper.Map<BrandModel>(updatedBrand);
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
                return await _brandRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return false; 
            }
        }
    }
}
