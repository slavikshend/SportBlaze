using webapi.BLL.Models;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Services.Interfaces
{
    public interface ILogReportService
    {
        Task<IEnumerable<LogLineModel>> GetAllLogLines();
        Task ClearAllLogs();
        Task<byte[]> GenerateProductCsv();
    }
}
