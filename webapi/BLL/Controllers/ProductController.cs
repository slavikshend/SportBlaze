using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.BLL.Helpers;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Controllers
{
    [Authorize(Roles = "Admin,RegisteredUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Create(ProductModel product)
        {
            var createdProduct = await _productService.CreateAsync(product);
            return Ok(createdProduct);
        }

        [HttpPut]
        public async Task<ActionResult<ProductModel>> Update(ProductModel product)
        {
            var updatedProduct = await _productService.UpdateAsync(product);
            if (updatedProduct != null)
            {
                return Ok(updatedProduct);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _productService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("special-offers")]
        public async Task<ActionResult<IEnumerable<SimplifiedProductModel>>> GetSpecialOffers()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var specialOfferProducts = await _productService.GetSpecialOfferProductsAsync(email);
            return Ok(specialOfferProducts);
        }

        [HttpGet("special-offers-anon")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetSpecialOffersAnon()
        {
            var email = "none";
            var specialOfferProducts = await _productService.GetSpecialOfferProductsAsync(email);
            return Ok(specialOfferProducts);
        }

        [HttpGet("{id}/details")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDetailsModel>> GetProductDetailsById(int id)
        {
            var productDetails = await _productService.GetProductDetailsByIdAsync(id);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SimplifiedProductModel>>> SearchProductsByName(string name)
        {
            var products = await _productService.SearchProductsByNameAsync(name);
            return Ok(products);
        }

        [HttpGet("personalized-recommendations")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetPersonalizedRecommendations()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email not found in claims.");
            }

            var recommendations = await _productService.GetPersonalizedRecommendationsAsync(email);
            return Ok(recommendations);
        }
    }


}
