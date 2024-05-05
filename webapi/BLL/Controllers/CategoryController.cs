using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICRUDService<CategoryModel> _categoryService;

        public CategoryController(ICRUDService<CategoryModel> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Create(CategoryModel category)
        {
            var createdCategory = await _categoryService.CreateAsync(category);
            if (createdCategory != null)
            {
                return Ok(createdCategory);
            }
            else
            {
                return StatusCode(500, "Failed to create category.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CategoryModel>> Update(CategoryModel category)
        {
            var updatedCategory = await _categoryService.UpdateAsync(category);
            if (updatedCategory != null)
            {
                return Ok(updatedCategory);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _categoryService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
