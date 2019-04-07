using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using ErmEngine.CsvApi.Models;
using ErmEngine.CsvApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErmEngine.CsvApi.Controllers
{
    [ApiController]
    public class CsvController : ControllerBase
    {
        private readonly ICsvService _CsvService;

        public CsvController(ICsvService CsvService)
        {
            _CsvService = CsvService;
        }

        [Route("api/GetSummary")]
        public async Task<IActionResult> GetSummary(string path, double threshold)
        {
            try
            {
                var anomalies = await _CsvService.GetAnomalies(path, threshold);
                var result = _CsvService.CreateSummaryFromAnomalies(anomalies);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}