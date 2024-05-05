using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ICRUDService<SubCategoryModel> _subCategoryService;

        public SubCategoryController(ICRUDService<SubCategoryModel> subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetAll()
        {
            var subCategories = await _subCategoryService.GetAllAsync();
            return Ok(subCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryModel>> GetById(int id)
        {
            var subCategory = await _subCategoryService.GetByIdAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return Ok(subCategory);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryModel>> Create(SubCategoryModel subCategory)
        {
            var createdSubCategory = await _subCategoryService.CreateAsync(subCategory);
            return Ok(createdSubCategory);
        }

        [HttpPut]
        public async Task<ActionResult<SubCategoryModel>> Update(SubCategoryModel subCategory)
        {
            var updatedSubCategory = await _subCategoryService.UpdateAsync(subCategory);
            if (updatedSubCategory != null)
            {
                return Ok(updatedSubCategory);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _subCategoryService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
