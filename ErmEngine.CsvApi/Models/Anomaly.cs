using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Models
{
    public class Anomaly
    {
        public string FileName { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public double Median { get; set; }

        public Anomaly(string fileName, DateTime dateTime, double value, double median)
        {
            FileName = fileName;
            DateTime = dateTime;
            Value = value;
            Median = median;
        }
    }
}
