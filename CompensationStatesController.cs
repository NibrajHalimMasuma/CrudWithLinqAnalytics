using CompensationWebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CompensationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompensationStatesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CompensationStatesController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet("/sum")]
        public IActionResult GetGroupedTotalGrossSalary()
        {
            var totalGrossSalary = db.CompensationDetails
                .GroupBy(p => p.PayMonth)
                .Select(g => new
                {
                    PayMonth = g.Key,
                    TotalGrossSalary = g.Sum(p => p.GrossSalary)
                })
                .ToList();

            return Ok(totalGrossSalary);
        }

        [HttpGet("/max")]
        public IActionResult GetGroupedMaxGrossSalary()
        {
            var maxGrossSalary = db.CompensationDetails
                .GroupBy(p => p.PayMonth)
                .Select(g => new
                {
                    PayMonth = g.Key,
                    MaxGrossSalary = g.Max(p => p.GrossSalary)
                })
                .ToList();

            return Ok(maxGrossSalary);
        }

        [HttpGet("/min")]
        public IActionResult GetGroupedMinGrossSalary()
        {
            var minGrossSalary = db.CompensationDetails
                .GroupBy(p => p.PayMonth)
                .Select(g => new
                {
                    PayMonth = g.Key,
                    MinGrossSalary = g.Min(p => p.GrossSalary)
                })
                .ToList();

            return Ok(minGrossSalary);
        }

        [HttpGet("/average")]
        public IActionResult GetGroupedAverageGrossSalary()
        {
            if (!db.CompensationDetails.Any())
                return NotFound("No Compensation data available.");

            var averageGrossSalary = db.CompensationDetails
                .GroupBy(p => p.PayMonth)
                .Select(g => new
                {
                    PayMonth = g.Key,
                    AverageGrossSalary = g.Average(p => p.GrossSalary)
                })
                .ToList();

            return Ok(averageGrossSalary);
        }

        [HttpGet("/count")]
        public IActionResult GetGroupedPayrollCount()
        {
            if (!db.CompensationDetails.Any())
                return NotFound("No Compensation data available.");

            var count = db.CompensationDetails
                .GroupBy(p => p.PayMonth)
                .Select(g => new
                {
                    PayMonth = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return Ok(count);
        }

        
        [HttpGet("/singlemax")]
        public IActionResult GetSingleMaxGrossSalary()
        {
            var maxGrossSalary = db.CompensationDetails
                .Max(p => p.GrossSalary); 

            return Ok(new { MaxGrossSalary = maxGrossSalary });
        }

       
        [HttpGet("/singlemin")]
        public IActionResult GetSingleMinGrossSalary()
        {
            var minGrossSalary = db.CompensationDetails
                .Min(p => p.GrossSalary); 

            return Ok(new { MinGrossSalary = minGrossSalary });
        }
    }
}
