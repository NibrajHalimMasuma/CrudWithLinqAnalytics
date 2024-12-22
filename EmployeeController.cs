using CompensationWebApi.Data;
using CompensationWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompensationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<EmployeeController> log;

        public EmployeeController(
            ApplicationDbContext ctx,

            ILogger<EmployeeController> logger
            )
        {
            db = ctx;
            log = logger;
        }

        [HttpPost("/upload")]
        public IActionResult Upload(IFormFile file, [FromServices] IWebHostEnvironment env)
        {
            try
            {
                if (file != null)
                {
                    string filePath = $"/images/{file.FileName}";
                    string uploadPath = env.WebRootPath + filePath;
                    using var stream = System.IO.File.Create(uploadPath);
                    file.CopyTo(stream);
                    return Ok(filePath);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {

            var data = db.Employees.Include(a => a.CompensationDetails).ToList();
            log.LogInformation("data retrieved success");
            return Ok(data);


        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = db.Employees.Include(a => a.CompensationDetails).FirstOrDefault(a => a.EmployeeId == id);
                log.LogInformation("data retrieved success");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Save(Employee emp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    log.LogInformation("data insert success");
                    return Ok(emp);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldData = db.Employees.Include(a => a.CompensationDetails).First(a => a.EmployeeId == emp.EmployeeId);
                    db.Employees.Remove(oldData);

                    db.Employees.Add(emp);
                    db.SaveChanges();
                    log.LogInformation("data update success");
                    return Ok(emp);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = db.Employees.Include(a => a.CompensationDetails).FirstOrDefault(a => a.EmployeeId == id);
                if (data == null)
                {
                    return NotFound($"Data not found by subject id {id}");
                }
                db.Employees.Remove(data);
                db.SaveChanges();
                log.LogTrace("data delete success");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

