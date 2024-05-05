using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Implementations
{
    public class ProductRepo : IProductRepo
    {
        private readonly SportsShopDbContext _context;

        public ProductRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.Features) 
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.Features) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(p => p.SubCategory).LoadAsync();
            await _context.Entry(entity).Reference(p => p.Brand).LoadAsync();
            await _context.Entry(entity).Collection(p => p.Features).LoadAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(p => p.SubCategory).LoadAsync();
            await _context.Entry(entity).Reference(p => p.Brand).LoadAsync();
            await _context.Entry(entity).Collection(p => p.Features).LoadAsync(); 
            return entity;
        }

        public async Task<IEnumerable<Product>> GetSpecialOfferProductsAsync()
        {
            return await _context.Products
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.Features)
                .Where(p => p.Discount > 0)
                .ToListAsync();
        }
    }
}
