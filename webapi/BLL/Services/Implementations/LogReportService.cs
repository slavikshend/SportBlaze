using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Implementations
{
    public class LogReportService : ILogReportService
    {
        private readonly ILogReportRepo _logReportRepo;
        private readonly ICategoryService _categoryService;
        private readonly ICRUDService<SubCategoryModel> _subcategoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICRUDService<BrandModel> _brandService;

        public LogReportService(ILogReportRepo logReportRepo,
                                ICategoryService categoryService,
                                ICRUDService<SubCategoryModel> subcategoryService,
                                IProductService productService,
                                IOrderService orderService,
                                ICRUDService<BrandModel> brandService)
        {
            _logReportRepo = logReportRepo;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _productService = productService;
            _orderService = orderService;
            _brandService = brandService;
        }

        public async Task<IEnumerable<LogLineModel>> GetAllLogLines()
        {
            var logLines = await _logReportRepo.GetAllLogLines();
            return logLines.Select(log => new LogLineModel
            {
                LogMessage = log.LogMessage,
                TimeStamp = log.TimeStamp
            });
        }

        public async Task ClearAllLogs()
        {
            await _logReportRepo.ClearAllLogs();
        }

        public async Task<byte[]> GenerateProductCsv()
        {
            var products = await _productService.GetAllAsync();
            var orders = await _orderService.GetAllOrders();
            var categories = await _categoryService.GetAllAsync();
            var subcategories = await _subcategoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();

            var csvData = new StringBuilder();

            // Products
            csvData.AppendLine("Products");
            csvData.AppendLine("Id,Name,Description,Price,Brand,Subcategory,Quantity,Discount");
            foreach (var product in products)
            {
                csvData.AppendLine($"{product.Id},{EscapeCsvField(product.Name)},{EscapeCsvField(product.Description)},{product.Price},{EscapeCsvField(product.BrandName)},{EscapeCsvField(product.SubCategoryName)},{product.Quantity},{product.Discount}");
            }

            csvData.AppendLine();

            // Orders
            csvData.AppendLine("Orders");
            csvData.AppendLine("Id,UserEmail,FirstName,LastName,PhoneNumber,DeliveryAddress,DeliveryName,PaymentName,Status,OrderDate,IsPaymentSuccessful,Total");
            foreach (var order in orders)
            {
                csvData.AppendLine($"{order.Id},{EscapeCsvField(order.UserEmail)},{EscapeCsvField(order.FirstName)},{EscapeCsvField(order.LastName)},{EscapeCsvField(order.PhoneNumber)},{EscapeCsvField(order.DeliveryAddress)},{EscapeCsvField(order.DeliveryName)},{EscapeCsvField(order.PaymentName)},{EscapeCsvField(order.Status)},{order.OrderDate},{order.IsPaymentSuccessful},{order.Total}");
            }

            csvData.AppendLine();

            // Categories
            csvData.AppendLine("Categories");
            csvData.AppendLine("Id,Name,Description");
            foreach (var category in categories)
            {
                csvData.AppendLine($"{category.Id},{EscapeCsvField(category.Name)}");
            }

            csvData.AppendLine();

            // Subcategories
            csvData.AppendLine("Subcategories");
            csvData.AppendLine("Id,Name,CategoryId");
            foreach (var subcategory in subcategories)
            {
                csvData.AppendLine($"{subcategory.Id},{EscapeCsvField(subcategory.Name)},{subcategory.CategoryId}");
            }

            csvData.AppendLine();

            // Brands
            csvData.AppendLine("Brands");
            csvData.AppendLine("Id,Name,Image");
            foreach (var brand in brands)
            {
                csvData.AppendLine($"{brand.Id},{EscapeCsvField(brand.Name)},{EscapeCsvField(brand.Image)}");
            }

            // Convert the CSV string to a byte array using UTF-8 encoding
            var csvString = csvData.ToString();
            byte[] csvBytes = Encoding.UTF8.GetBytes(csvString);
            return csvBytes;
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return "";
            }

            if (field.Contains(",") || field.Contains("\n") || field.Contains("\""))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            else
            {
                return field;
            }
        }
    }
}
