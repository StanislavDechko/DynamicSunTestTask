using System.Linq;
using System.Threading.Tasks;
using DynamicSunTestTask.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTestTask.Controllers
{
    public class DataController : Controller
    {
        private readonly SqlServerDataAccessService _dataAccessService;

        public DataController(SqlServerDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int year, int month)
        {
            if (year == 0)
            {
                return View(null);
            }

            var records = await _dataAccessService.GetRecordsAsync(year, month);
            
            return View(records);
        }
    }
}
