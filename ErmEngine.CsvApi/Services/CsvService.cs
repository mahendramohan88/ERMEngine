using CsvHelper;
using ErmEngine.CsvApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErmEngine.CsvApi.Services
{
    public class CsvService : ICsvService
    {
        private readonly ILpService _lpService;
        private readonly ITouService _touService;

        public CsvService(ILpService lpService, ITouService touService)
        {
            _lpService = lpService;
            _touService = touService;
        }

        // Unit tests require a constructor with no parameters
        public CsvService() { }

        public async Task<List<Anomaly>> GetAnomalies(string path, double thresholdMultiplier)
        {
            if (thresholdMultiplier < 0)
            {
                throw new ArgumentException("Please specify a positive threshold. For example 0.2 will set the threshold at 20% above and below the median.");
            }

            var fileName = Path.GetFileName(path);

            if (fileName.Contains("LP_"))
            {
                return await _lpService.GetAnomalies(path, thresholdMultiplier);
            }

            if (fileName.Contains("TOU_"))
            {
                return await _touService.GetAnomalies(path, thresholdMultiplier);
            }

            throw new IOException("Filename does not start with a valid option. It should start with either LP_ or TOU_ depending on the type of CSV file that is being input.");
        }

        public string CreateSummaryFromAnomalies(List<Anomaly> anomalies)
        {
            var summary = String.Empty;

            foreach(var anomaly in anomalies)
            {
                summary += String.Format("{0}\t{1}\t{2}\t{3}\n", anomaly.FileName, anomaly.DateTime.ToString(), anomaly.Value, anomaly.Median);
            }

            return summary;
        }

        
    }
}
