using System.Collections.Generic;
using System.Threading.Tasks;
using ErmEngine.CsvApi.Models;

namespace ErmEngine.CsvApi.Services
{
    public interface ILpService
    {
        Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier);
        List<List<LpRowItem>> GetGroupedRows(List<LpRowItem> rows);
        Dictionary<string, double> GetGroupMedians(List<List<LpRowItem>> groupedRows);
        double GetMedian(IEnumerable<double> values);
        Task<List<LpRowItem>> GetRowsFromCsv(string path);
    }
}