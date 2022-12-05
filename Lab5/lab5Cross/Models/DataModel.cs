using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5Cross.Models
{
    public class DataModel
    {
        public string[] Data { get; set; }
        public string ErrorValue { get; set; }
        public string Response { get; set; }
        public bool Calculated { get; set; }
    }
}
