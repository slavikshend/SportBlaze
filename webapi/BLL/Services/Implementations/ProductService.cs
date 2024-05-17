using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using webapi.BLL.Helpers;
using webapi.BLL.Models;
using webapi.BLL.Repos.Implementations;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly RecommendationSystem _recommendationSystem;

        public ProductService(IProductRepo productRepo, IMapper mapper, IUserRepo userRepo, RecommendationSystem recommendationSystem)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _recommendationSystem = recommendationSystem ?? throw new ArgumentNullException(nameof(recommendationSystem));
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var products = await _productRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task<ProductModel> CreateAsync(ProductModel entity)
        {
            var productEntity = _mapper.Map<Product>(entity);
            var createdProduct = await _productRepo.CreateAsync(productEntity);
            return _mapper.Map<ProductModel>(createdProduct);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel entity)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(entity);
                var updatedProduct = await _productRepo.UpdateAsync(productEntity);
                return _mapper.Map<ProductModel>(updatedProduct);
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
                return await _productRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SimplifiedProductModel>> GetSpecialOfferProductsAsync(string email)
        {
            var specialOfferProducts = await _productRepo.GetSpecialOfferProductsAsync();
            var simplifiedProducts = _mapper.Map<IEnumerable<SimplifiedProductModel>>(specialOfferProducts);

            foreach (var product in simplifiedProducts)
            {
                product.IsFavourite = specialOfferProducts.Any(p => p.Id == product.Id && p.Favourites.Any(f => f.UserEmail == email));

            }

            return simplifiedProducts;
        }

        public async Task<ProductDetailsModel> GetProductDetailsByIdAsync(int id)
        {
            var product = await _productRepo.GetProductDetailsByIdAsync(id);
            return _mapper.Map<ProductDetailsModel>(product);
        }

        public async Task<IEnumerable<SimplifiedProductModel>> SearchProductsByNameAsync(string name)
        {
            var products = await _productRepo.SearchProductsByNameAsync(name);
            return _mapper.Map<IEnumerable<SimplifiedProductModel>>(products);
        }
        public async Task<IEnumerable<ProductModel>> GetPersonalizedRecommendationsAsync(string userEmail)
        {
            var targetUser = await _userRepo.GetRegisteredUser(userEmail);
            if (targetUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var recommendations = await _recommendationSystem.GetRecommendations(targetUser.Email);
            return _mapper.Map<IEnumerable<ProductModel>>(recommendations);
        }

        public async Task<IEnumerable<SimplifiedProductModel>> GetProductsBySubcategoryAsync(string subcategory)
        {
            var products = await _productRepo.GetProductsBySubcategoryAsync(subcategory);
            return _mapper.Map<IEnumerable<SimplifiedProductModel>>(products);
        }

    }
}
