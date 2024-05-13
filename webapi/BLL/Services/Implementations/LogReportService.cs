using System.Collections.Generic;
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
        private readonly IProductService _productService;

        public LogReportService(ILogReportRepo logReportRepo, IProductService productService)
        {
            _logReportRepo = logReportRepo;
            _productService = productService;
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
            var csvData = new StringBuilder();

            csvData.AppendLine("Id,Назва,Опис,Ціна,Бренд,Підкатегорія,Кількість,Знижка");

            foreach (var product in products)
            {
                csvData.AppendLine($"{product.Id},{EscapeCsvField(product.Name)},{EscapeCsvField(product.Description)},{product.Price},{EscapeCsvField(product.BrandName)},{EscapeCsvField(product.SubCategoryName)}, {EscapeCsvField(product.Quantity.ToString())}, {EscapeCsvField(product.Discount.ToString())}");
            }

            csvData.AppendLine();

            csvData.AppendLine("Таблиця логів системи");
            csvData.AppendLine("Час,Повідомлення");
            IEnumerable<LogLineModel> logLines = await GetAllLogLines();

            foreach (var logLine in logLines)
            {
                csvData.AppendLine($"{logLine.TimeStamp},{EscapeCsvField(logLine.LogMessage)}");
            }

            return Encoding.UTF8.GetBytes(csvData.ToString());
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
