using ErmEngine.CsvApi.Models;
using ErmEngine.CsvApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ErmEngine.UnitTests
{
    [TestClass]
    public class TouServiceTests
    {
        private readonly ITouService _touService;

        public TouServiceTests()
        {
            _touService = new TouService();
        }

        [DataTestMethod]
        [DataRow(new double[]{ 0, 1, 2, 3, 4, 5 }, 2.5)]
        [DataRow(new double[] { 1, 2, 3, 4, 5 }, 3)]
        [DataRow(new double[] { 27.59, 34.42, 30.9, 25.94, 31.19 }, 30.9)]
        public void GetMedian_ReturnsCorrectValue(IEnumerable<double> values, double expected)
        {
            var actual = _touService.GetMedian(values);

            Assert.AreEqual(expected, actual);
        }


        public List<TouRowItem> GetSampleRows()
        {
            return new List<TouRowItem>
            {
                new TouRowItem(DateTime.Now, 27.59),
                new TouRowItem(DateTime.Now, 34.42),
                new TouRowItem(DateTime.Now, 30.9),
                new TouRowItem(DateTime.Now, 25.94),
                new TouRowItem(DateTime.Now, 31.19)
            };
        }
    }
}
