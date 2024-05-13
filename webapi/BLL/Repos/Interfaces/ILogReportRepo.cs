using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Interfaces
{
    public interface ILogReportRepo
    {
        Task<IEnumerable<LogLine>> GetAllLogLines();
        Task ClearAllLogs();
    }
}
