using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieDbContextDAO _movieDbContext;

        public MovieController(IMovieDbContextDAO movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(_movieDbContext.GetAllRecords());
        }

        [HttpGet("id")]
        public IActionResult GetRecordById(int id)
        {
            var record = _movieDbContext.GetRecordById(id);

            if (record == null)
                return NotFound(id);

            return Ok(record);
        }

        [HttpPut]
        public IActionResult UpdateRecord(Movie movie)
        {
            int? result = _movieDbContext.UpdateRecord(movie);

            if (result == null)
                return NotFound(movie);
            if (result == 0)
                return StatusCode(500, "Update failed. Please try again.");

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRecord(int id)
        {
            int? result = _movieDbContext.RemoveRecord(id);

            if (result == null)
                return NotFound(id);
            if (result == 0)
                return StatusCode(500, "Delete failed. Please try again.");

            return Ok();
        }

        [HttpPost]
        public IActionResult AddRecord(Movie movie)
        {
            int? result = _movieDbContext.AddRecord(movie);

            if (result == null)
                return StatusCode(500, "Add failed. Record already exists.");
            if (result == 0)
                return StatusCode(500, "Add failed. Please ensure the id field is set to 0, then try again.");

            return Ok();
        }
    }
}
