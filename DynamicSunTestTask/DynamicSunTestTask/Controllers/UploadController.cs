using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicSunTestTask.Contexts;
using DynamicSunTestTask.Models;
using DynamicSunTestTask.Services;
using DynamicSunTestTask.Services.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.UserModel;

namespace DynamicSunTestTask.Controllers
{
    public class UploadController : Controller
    {
        private readonly SqlServerDataAccessService _dataAccessService;

        public UploadController(SqlServerDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> uploads)
        {
            var allFailedRecords = new Dictionary<string, List<string>>();
            foreach (var upload in uploads)
            {
                try
                {
                    var (records, failedRecords) = ExcelService.GetRecords(upload);

                    if (failedRecords.Errors.Any())
                    {
                        allFailedRecords.Add(failedRecords.FileName, failedRecords.Errors);
                    }

                    await _dataAccessService.SaveRecordsAsync(records);
                }
                catch (DbUpdateException ex)
                {
                    return View("Error", new ErrorViewModel
                    {
                        InnerError = $"Error while saving records:",
                        Errors = new Dictionary<string, List<string>>
                        {
                            { upload.FileName, new List<string>{ ex.Message } }
                        },
                    });
                }
                catch (Exception ex)
                {
                    return View("Error", new ErrorViewModel
                    {
                        InnerError = $"{ex.Message} in {upload.FileName}",
                        Errors = new Dictionary<string, List<string>>(),
                    });
                }
            }

            if (allFailedRecords.Any())
            {
                return View("Error", new ErrorViewModel
                {
                    InnerError = "An error occurred while parsing files:",
                    Errors = allFailedRecords
                });
            }

            return Redirect("../Data/GetData");
        }
    }
}
