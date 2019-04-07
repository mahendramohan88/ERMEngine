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
        // The threshold here could be made a parameter for the query as well.
        private const double THRESHOLD = 0.2;

        private readonly ICsvService _CsvService;

        public CsvController(ICsvService CsvService)
        {
            _CsvService = CsvService;
        }

        [Route("api/GetSummary"), HttpPost]
        public async Task<IActionResult> GetSummary([FromBody]string path)
        {
            try
            {
                var anomalies = await _CsvService.GetAnomalies(path, THRESHOLD);
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