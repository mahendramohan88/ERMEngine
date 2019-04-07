using CsvHelper;
using ErmEngine.CsvApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Services
{
    public class LpService : ILpService
    {
        public async Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier)
        {
            var fileName = Path.GetFileName(path);
            var rows = await GetRowsFromCsv(path);
            var groupedRows = GetGroupedRows(rows).ToList();
            var groupMedians = GetGroupMedians(groupedRows);
            var anomalies = new List<Anomaly>();

            foreach (var group in groupedRows)
            {
                var dataType = group.FirstOrDefault().DataType;
                var values = group.Select(x => x.DataValue);
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
                group.Where(x => x.DataValue > thresholdHigh || x.DataValue < thresholdLow).ToList().ForEach(x =>
                {
                    anomalies.Add(new Anomaly(fileName, x.DateTime, x.DataValue, median));
                });
            }

            return anomalies;
        }


        public async Task<List<LpRowItem>> GetRowsFromCsv(string path)
        {
            var rows = new List<LpRowItem>();

            using (TextReader reader = File.OpenText(path))
            {
                CsvReader csvReader = new CsvReader(reader);
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.MissingFieldFound = null;
                csvReader.Configuration.HeaderValidated = null;
                while (csvReader.Read())
                {
                    var row = csvReader.GetRecord<LpRowItem>();
                    rows.Add(row);
                }
            }

            return rows;
        }

        public List<List<LpRowItem>> GetGroupedRows(List<LpRowItem> rows)
        {
            var dataTypeGroups = rows.GroupBy(x => x.DataType)
                .Select(group => group.ToList())
                .ToList();

            return dataTypeGroups;
        }

        public Dictionary<string, double> GetGroupMedians(List<List<LpRowItem>> groupedRows)
        {
            var result = new Dictionary<string, double>();

            groupedRows.ForEach(group =>
            {
                var values = group.Select(x => x.DataValue);
                var median = GetMedian(values);
                var dataType = group.FirstOrDefault().DataType;

                result.Add(dataType, median);
            });

            return result;
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
