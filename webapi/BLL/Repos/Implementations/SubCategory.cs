using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Implementations
{
    public class SubCategoryRepo : ICRUDRepo<SubCategory>
    {
        private readonly SportsShopDbContext _context;

        public SubCategoryRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            _context.SubCategories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
                return false;

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _context.SubCategories.FindAsync(id);
        }

        public async Task<SubCategory> UpdateAsync(SubCategory entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<SubCategory>> GetByCategoryNameAsync(string categoryName)
        {
            return await _context.SubCategories
                .Include(sc => sc.Category)
                .Where(sc => sc.Category.Name == categoryName)
                .ToListAsync();
        }
    }
}
