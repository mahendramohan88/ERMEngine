using ErmEngine.CsvApi.Models;
using ErmEngine.CsvApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ErmEngine.UnitTests
{
    [TestClass]
    public class CsvServiceTests
    {
        private readonly ICsvService _csvService;

        public CsvServiceTests()
        {
            _csvService = new CsvService();
        }

        [TestMethod]
        public async Task GetAnomalies_FailsIfThresholdMultiplierNegative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _csvService.GetAnomalies("", -0.1));
        }

        [TestMethod]
        public async Task GetAnomalies_FailsIfFileNameInvalid()
        {
            await Assert.ThrowsExceptionAsync<IOException>(() => _csvService.GetAnomalies("", 0.2));
        }

        [TestMethod]
        public void CreateSummaryFromAnomalies_ReturnsCorrectString()
        {
            var anomalies = GetSampleAnomalies();
            var expected = GetSampleString();
            var actual = _csvService.CreateSummaryFromAnomalies(anomalies);

            Assert.AreEqual(expected, actual);
        }

        public List<Anomaly> GetSampleAnomalies()
        {
            return new List<Anomaly>
            {
                new Anomaly("TestFileName.csv", new DateTime(2020, 01, 01), 56.48, 40),
                new Anomaly("TestFileName.csv", new DateTime(2020, 01, 01), 55.48, 40),
                new Anomaly("TestFileName.csv", new DateTime(2020, 01, 01), 54.48, 40),
                new Anomaly("TestFileName.csv", new DateTime(2020, 01, 01), 53.48, 40)
            };
        }

        public string GetSampleString()
        {
            var result = String.Empty;
            result += "TestFileName.csv\t1/01/2020 12:00:00 AM\t56.48\t40\n";
            result += "TestFileName.csv\t1/01/2020 12:00:00 AM\t55.48\t40\n";
            result += "TestFileName.csv\t1/01/2020 12:00:00 AM\t54.48\t40\n";
            result += "TestFileName.csv\t1/01/2020 12:00:00 AM\t53.48\t40\n";
            return result;
        }
    }
}
