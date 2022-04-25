using System;
using System.Linq;
using System.Reflection;
using DynamicSunTestTask.Models;
using NPOI.HPSF;
using NPOI.SS.UserModel;

namespace DynamicSunTestTask.Extensions
{
    public static class IRowExtensions
    {
        public static Record ToRecordObject(this IRow row)
        {
            var date = DateTime.Parse($"{row.GetCell(0)} {row.GetCell(1)}");
            var temperature = GetValue(row.GetCell(2));
            var relativeHumidity = GetValue(row.GetCell(3));
            var dewPoint = GetValue(row.GetCell(4));
            var pressure = GetValue(row.GetCell(5));
            var windDirection = GetValue(row.GetCell(6));
            var windSpeed = GetValue(row.GetCell(7));
            var cloudiness = GetValue(row.GetCell(8));
            var cloudinessLowerBound = GetValue(row.GetCell(9));
            var horizontalVisibility = GetValue(row.GetCell(10))?.ToString();
            var naturalPhenomena = GetValue(row.GetCell(11));
            return new Record
            {
                Date = date,
                Temperature = temperature,
                RelativeHumidity = relativeHumidity,
                DewPoint = dewPoint,
                Pressure = pressure,
                WindDirection = windDirection,
                WindSpeed = windSpeed,
                Cloudiness = cloudiness,
                CloudinessLowerBound = cloudinessLowerBound,
                HorizontalVisibility = horizontalVisibility,
                NaturalPhenomena = naturalPhenomena
            };
        }

        private static dynamic GetValue(ICell cell) => cell?.CellType switch
        {
            null => null,
            CellType.Blank => null,
            CellType.Boolean => cell.BooleanCellValue,
            CellType.Numeric => cell.NumericCellValue,
            CellType.String => string.IsNullOrWhiteSpace(cell.StringCellValue) ? null : cell.StringCellValue,
            _ => throw new ArgumentException("Unknown cell type")
        };
    }
}
