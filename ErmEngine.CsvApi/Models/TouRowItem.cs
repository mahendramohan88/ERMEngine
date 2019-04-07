using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Models
{
    public class TouRowItem
    {
        public TouRowItem(DateTime dateTime, double energy)
        {
            DateTime = dateTime;
            Energy = energy;
        }

        public int MeterPointCode { get; set; }
        public int SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public string DataType { get; set; }
        public double Energy { get; set; }
        public double Maximum { get; set; }
        public DateTime MaxDemandTime { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
        public string Period { get; set; }
        public bool DLSActive { get; set; }
        public int BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
