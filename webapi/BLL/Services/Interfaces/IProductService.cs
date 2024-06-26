﻿using webapi.BLL.Models;

namespace webapi.BLL.Services.Interfaces
{
    public interface IProductService : ICRUDService<ProductModel>
    {
        Task<IEnumerable<SimplifiedProductModel>> GetSpecialOfferProductsAsync(string email);
        Task<ProductDetailsModel> GetProductDetailsByIdAsync(int id);
        Task<IEnumerable<SimplifiedProductModel>> SearchProductsByNameAsync(string name);
        Task<IEnumerable<ProductModel>> GetPersonalizedRecommendationsAsync(string userEmail);
        Task<IEnumerable<SimplifiedProductModel>> GetProductsBySubcategoryAsync(string subcategory);
    }
}
