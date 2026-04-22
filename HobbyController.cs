using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly IHobbyDbContextDAO _hobbyDbContext;

        public HobbyController(IHobbyDbContextDAO hobbyDbContext)
        {
            _hobbyDbContext = hobbyDbContext;
        }

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(_hobbyDbContext.GetAllRecords());
        }

        [HttpGet("id")]
        public IActionResult GetRecordById(int id)
        {
            var record = _hobbyDbContext.GetRecordById(id);

            if (record == null)
                return NotFound(id);

            return Ok(record);
        }

        [HttpPut]
        public IActionResult UpdateRecord(Hobby hobby)
        {
            int? result = _hobbyDbContext.UpdateRecord(hobby);

            if (result == null)
                return NotFound(hobby);
            if (result == 0)
                return StatusCode(500, "Update failed. Please try again.");

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRecord(int id)
        {
            int? result = _hobbyDbContext.RemoveRecord(id);

            if (result == null)
                return NotFound(id);
            if (result == 0)
                return StatusCode(500, "Delete failed. Please try again.");

            return Ok();
        }

        [HttpPost]
        public IActionResult AddRecord(Hobby hobby)
        {
            int? result = _hobbyDbContext.AddRecord(hobby);

            if (result == null)
                return StatusCode(500, "Add failed. Record already exists.");
            if (result == 0)
                return StatusCode(500, "Add failed. Please ensure the id field is set to 0, then try again.");

            return Ok();
        }
    }
}
