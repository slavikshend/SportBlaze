using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Repos.Implementations
{
    public class LogReportRepo : ILogReportRepo
    {
        private readonly SportsShopDbContext _context;

        public LogReportRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogLine>> GetAllLogLines()
        {
            return await _context.LogLines.ToListAsync();
        }

        public async Task ClearAllLogs()
        {
            _context.LogLines.RemoveRange(await _context.LogLines.ToListAsync());
            await _context.SaveChangesAsync();
        }
    }
}
