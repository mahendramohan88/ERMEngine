using CsvHelper;
using ErmEngine.CsvApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Services
{
    public class TouService : ITouService
    {
        public async Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier)
        {
            var fileName = Path.GetFileName(path);
            var rows = await GetRowsFromCsv(path);
            var anomalies = new List<Anomaly>();

            var values = rows.Select(x => x.Energy);
            var median = GetMedian(values);

            var threshold = median * thresholdMultiplier;
            var thresholdHigh = median + threshold;
            var thresholdLow = median - threshold;

            // Flip thresholds if negative values
            if (median < 0)
            {
                var thresholdTemp = thresholdHigh;
                thresholdHigh = thresholdLow;
                thresholdLow = thresholdTemp;
            }

            // Detect anomalies and add to result
            rows.Where(x => x.Energy > thresholdHigh || x.Energy < thresholdLow).ToList().ForEach(x =>
            {
                anomalies.Add(new Anomaly(fileName, x.DateTime, x.Energy, median));
            });

            return anomalies;
        }


        public async Task<List<TouRowItem>> GetRowsFromCsv(string path)
        {
            var rows = new List<TouRowItem>();

            using (TextReader reader = File.OpenText(path))
            {
                CsvReader csvReader = new CsvReader(reader);
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.MissingFieldFound = null;
                csvReader.Configuration.HeaderValidated = null;
                while (csvReader.Read())
                {
                    var row = csvReader.GetRecord<TouRowItem>();
                    rows.Add(row);
                }
            }

            return rows;
        }

        public double GetMedian(IEnumerable<double> values)
        {
            var sortedList = values.OrderBy(x => x);
            int count = sortedList.Count();
            int itemIndex = count / 2;

            // Even number of items
            if (count % 2 == 0)
            {
                return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
            }

            // Odd number of items 
            return sortedList.ElementAt(itemIndex);
        }

    }
}
