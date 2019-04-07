using ErmEngine.CsvApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Services
{
    public interface ICsvService
    {
        Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier);
        string CreateSummaryFromAnomalies(List<Anomaly> anomalies);
    }
}