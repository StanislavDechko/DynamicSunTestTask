using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicSunTestTask.Contexts;
using DynamicSunTestTask.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace DynamicSunTestTask.Services.DataAccess
{
    public class SqlServerDataAccessService
    {
        private readonly RecordsContext _context;

        public SqlServerDataAccessService(RecordsContext context)
        {
            _context = context;
        }

        public async Task SaveRecordsAsync(List<Record> records)
        {
            await _context.Records.AddRangeAsync(records);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Record>> GetRecordsAsync(int year, int month) => month switch
        {
            0 => await  _context.Records
                .Where(record => record.Date.Year == year)
                .ToListAsync(),
            _ => await _context.Records
                .Where(record => record.Date.Year == year && record.Date.Month == month)
                .ToListAsync()
        };
    }
}
