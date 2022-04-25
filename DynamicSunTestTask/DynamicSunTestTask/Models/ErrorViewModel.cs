using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DynamicSunTestTask.Models
{
    public class ErrorViewModel
    {
        public string InnerError { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
