using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.BLL.Services.Interfaces;
using webapi.BLL.Models;
using System.Text;
using webapi.BLL.Services.Implementations;

namespace webapi.BLL.Controllers
{
    [ApiController]
    [Route("api/logreport")]
    [Authorize(Roles = "Admin")]
    public class LogReportController : ControllerBase
    {
        private readonly ILogReportService _logReportService;
        private readonly IProductService _productService;

        public LogReportController(ILogReportService logReportService, IProductService productService)
        {
            _logReportService = logReportService;
            _productService = productService;
        }

        [HttpGet("loglines")]
        public async Task<ActionResult<IEnumerable<LogLineModel>>> GetAllLogLines()
        {
            var logLines = await _logReportService.GetAllLogLines();
            return Ok(logLines);
        }

        [HttpPost("clearlogs")]
        public async Task<IActionResult> ClearAllLogs()
        {
            await _logReportService.ClearAllLogs();
            return Ok();
        }

        [HttpGet("productcsv")]
        public async Task<IActionResult> GetProductCsv()
        {
            var csvData = await _logReportService.GenerateProductCsv();
            return File(csvData, "text/csv", "products.csv");
        }

    }
}
