﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.Include(sc => sc.Category).ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _context.SubCategories.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
        }

         public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            _context.SubCategories.Add(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(sc => sc.Category).LoadAsync();
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

        public async Task<SubCategory> UpdateAsync(SubCategory entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            await _context.Entry(entity).Reference(sc => sc.Category).LoadAsync();
            return entity;
        }
    }
}
