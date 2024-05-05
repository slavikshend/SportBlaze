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
    public class BrandController : ControllerBase
    {
        private readonly ICRUDService<BrandModel> _service;

        public BrandController(ICRUDService<BrandModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandModel>>> GetAll()
        {
            var brands = await _service.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandModel>> GetById(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandModel>> Create(BrandModel brand)
        {
            var createdBrand = await _service.CreateAsync(brand);
            if (createdBrand != null)
            {
                return Ok(createdBrand);
            }
            else
            {
                return StatusCode(500, "Failed to create brand.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<BrandModel>> Update(BrandModel brand)
        {
            var updatedBrand = await _service.UpdateAsync(brand);
            if (updatedBrand != null)
            {
                return Ok(updatedBrand);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
