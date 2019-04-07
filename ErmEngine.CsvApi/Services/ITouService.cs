using System.Collections.Generic;
using System.Threading.Tasks;
using ErmEngine.CsvApi.Models;

namespace ErmEngine.CsvApi.Services
{
    public interface ITouService
    {
        Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier);
        double GetMedian(IEnumerable<double> values);
        Task<List<TouRowItem>> GetRowsFromCsv(string path);
    }
}