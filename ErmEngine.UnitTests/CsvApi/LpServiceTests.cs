using ErmEngine.CsvApi.Models;
using ErmEngine.CsvApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ErmEngine.UnitTests
{
    [TestClass]
    public class LpServiceTests
    {
        private readonly ILpService _lpService;

        public LpServiceTests()
        {
            _lpService = new LpService();
        }

        [TestMethod]
        public void GetGroupedRows_ReturnsCorrectGroups()
        {
            var rows = GetSampleRows();
            var groupedRows = _lpService.GetGroupedRows(rows);

            var group1 = groupedRows[0];
            var group2 = groupedRows[1];

            var expectedGroup1 = "Phase Angle B";
            var expectedGroup2 = "Voltage Phase C Min";

            var actualGroup1 = group1[0].DataType;
            var actualGroup2 = group2[0].DataType;

            Assert.AreEqual(expectedGroup1, actualGroup1);
            Assert.AreEqual(expectedGroup2, actualGroup2);
        }

        [TestMethod]
        public void GetGroupedRows_ReturnsCorrectGroupCount()
        {
            var rows = GetSampleRows();
            var groupedRows = _lpService.GetGroupedRows(rows);

            var expected = 2;
            var actual = groupedRows.Count;

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("Phase Angle B", 30.9)]
        [DataRow("Voltage Phase C Min", 238.76)]
        public void GetGroupMedians_ReturnsCorrectMedians(string key, double expected)
        {
            var rows = GetSampleRows();
            var groupedRows = _lpService.GetGroupedRows(rows);
            var groupMedians = _lpService.GetGroupMedians(groupedRows);

            var actual = groupMedians.GetValueOrDefault(key);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(new double[]{ 0, 1, 2, 3, 4, 5 }, 2.5)]
        [DataRow(new double[] { 1, 2, 3, 4, 5 }, 3)]
        [DataRow(new double[] { 27.59, 34.42, 30.9, 25.94, 31.19 }, 30.9)]
        public void GetMedian_ReturnsCorrectValue(IEnumerable<double> values, double expected)
        {
            var actual = _lpService.GetMedian(values);

            Assert.AreEqual(expected, actual);
        }


        public List<LpRowItem> GetSampleRows()
        {
            return new List<LpRowItem>
            {
                new LpRowItem(DateTime.Now, "Phase Angle B", 27.59),
                new LpRowItem(DateTime.Now, "Phase Angle B", 34.42),
                new LpRowItem(DateTime.Now, "Phase Angle B", 30.9),
                new LpRowItem(DateTime.Now, "Phase Angle B", 25.94),
                new LpRowItem(DateTime.Now, "Phase Angle B", 31.19),
                new LpRowItem(DateTime.Now, "Voltage Phase C Min", 0),
                new LpRowItem(DateTime.Now, "Voltage Phase C Min", 240.19),
                new LpRowItem(DateTime.Now, "Voltage Phase C Min", 238.76),
                new LpRowItem(DateTime.Now, "Voltage Phase C Min", 238.96),
                new LpRowItem(DateTime.Now, "Voltage Phase C Min", 238.7)
            };
        }
    }
}
