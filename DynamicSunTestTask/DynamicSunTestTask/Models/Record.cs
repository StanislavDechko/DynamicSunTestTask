using System;
using System.ComponentModel.DataAnnotations;

namespace DynamicSunTestTask.Models
{
    public class Record
    {
        [Key]
        public DateTime Date { get; set; }
        
        public double? Temperature { get; set; }
        
        public double? RelativeHumidity { get; set; }
        
        public double? DewPoint { get; set; }
        
        public double? Pressure { get; set; }
        
        public string WindDirection { get; set; }
        
        public double? WindSpeed { get; set; }
        
        public double? Cloudiness { get; set; }
        
        public double? CloudinessLowerBound { get; set; }
        
        public string HorizontalVisibility { get; set; }
        
        public string NaturalPhenomena { get; set; }
    }
}
