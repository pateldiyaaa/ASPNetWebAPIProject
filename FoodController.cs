using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodDbContextDAO _foodDbContext;

        public FoodController(IFoodDbContextDAO foodDbContext)
        {
            _foodDbContext = foodDbContext;
        }

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(_foodDbContext.GetAllRecords());
        }

        [HttpGet("id")]
        public IActionResult GetRecordById(int id)
        {
            var record = _foodDbContext.GetRecordById(id);

            if (record == null)
                return NotFound(id);

            return Ok(record);
        }

        [HttpPut]
        public IActionResult UpdateRecord(Food food)
        {
            int? result = _foodDbContext.UpdateRecord(food);

            if (result == null)
                return NotFound(food);
            if (result == 0)
                return StatusCode(500, "Update failed. Please try again.");

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRecord(int id)
        {
            int? result = _foodDbContext.RemoveRecord(id);

            if (result == null)
                return NotFound(id);
            if (result == 0)
                return StatusCode(500, "Delete failed. Please try again.");

            return Ok();
        }

        [HttpPost]
        public IActionResult AddRecord(Food food)
        {
            int? result = _foodDbContext.AddRecord(food);

            if (result == null)
                return StatusCode(500, "Add failed. Record already exists.");
            if (result == 0)
                return StatusCode(500, "Add failed. Please ensure the id field is set to 0, then try again.");

            return Ok();
        }
    }
}
