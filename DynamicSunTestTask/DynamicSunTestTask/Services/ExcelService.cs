using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using DynamicSunTestTask.Extensions;
using DynamicSunTestTask.Models;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Org.BouncyCastle.Asn1.Cmp;

namespace DynamicSunTestTask.Services
{
    public static class ExcelService
    {
        public static (List<Record> Records, (string FileName, List<string> Errors) FailedRecords) GetRecords(IFormFile upload)
        {
            using var memoryStream = new MemoryStream();
            upload.CopyTo(memoryStream);
            memoryStream.Position = 0;

            IWorkbook workbook = Path.GetExtension(upload.FileName) switch
            {
                ".xlsx" => new XSSFWorkbook(memoryStream),
                ".xls" => new HSSFWorkbook(memoryStream),
                _ => throw new ArgumentException($"`{upload.FileName}` is not supported file type."),
            };

            var (records, failedRecords) = GetRecords(workbook);

            return (records, (upload.FileName, failedRecords));
        }

        private static (List<Record>, List<string>) GetRecords(IWorkbook workbook)
        {
            var records = new List<Record>();
            var failedRecords = new List<string>();
            foreach (var sheet in workbook)
            {
                for (int row = 4; row < sheet.LastRowNum; row++)
                {
                    try
                    {
                        var record = sheet.GetRow(row).ToRecordObject();
                        records.Add(record);
                    }
                    catch
                    {
                        failedRecords.Add($"In '{sheet.SheetName}' at row {row}");
                    }
                }
            }

            return (records, failedRecords);
        }
    }
}
