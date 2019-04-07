using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Models
{
    public class LpRowItem
    {
        public LpRowItem(DateTime dateTime, string dataType, double dataValue)
        {
            DateTime = dateTime;
            DataType = dataType;
            DataValue = dataValue;
        }

        public LpRowItem() { }

        [Name("MeterPoint Code")]
        public int MeterPointCode { get; set; }
        [Name("Serial Number")]
        public int SerialNumber { get; set; }
        [Name("Plant Code")]
        public string PlantCode { get; set; }
        [Name("Date/Time")]
        public DateTime DateTime { get; set; }
        [Name("Data Type")]
        public string DataType { get; set; }
        [Name("Data Value")]
        public double DataValue { get; set; }
        [Name("Units")]
        public string Units { get; set; }
        [Name("Status")]
        public string Status { get; set; }
    }
}
