using FinalProject.Data;
using FinalProject.Interfaces;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    //Sami - controller url: localhost:7071/api/Team
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        //Sami Cox - DBContext contructor
        private readonly ITeamDbContextDAO _teamDbContext;
        public TeamController(ITeamDbContextDAO teamDbContext)
        {
            this._teamDbContext = teamDbContext;
        }

        //Sami - READ all data in TeamMember table by calling GetAllMembers method from interface
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            return Ok(_teamDbContext.GetAllMembers());
        }

        //Sami - READ data in TeamMember table of input ID by calling GetMember method from interface
        [HttpGet("id")]
        public IActionResult GetMemberById(int? id)
        {
            if (id == null || id == 0)
            {
                int i = 1;
                var firstFive = new List<TeamMember>();
                while (i <= 5)
                {
                    var firstFiveMembers = _teamDbContext.GetMember(i);
                    firstFive.Add(firstFiveMembers);
                    i++;
                }
                return Ok(firstFive);
            }
            var teamMember = _teamDbContext.GetMember(id);

            if (teamMember == null)
                return NotFound(id);

            return Ok(teamMember);
        }

        //Sami - PUT/Update data in TeamMember table by calling UpdateMember method from interface
        [HttpPut]
        public IActionResult PutMember(TeamMember teamMember)
        {
            int? result = _teamDbContext.UpdateMember(teamMember);

            if (result == null)
                return NotFound(teamMember);
            if (result == 0)
                return StatusCode(500,"Update failed. Please try again.");
            return Ok();
        }

        //Sami - DELETE data in TeamMember table of input ID by calling RemoveMember method from interface
        [HttpDelete("id")]
        public IActionResult DeleteMemberById(int id)
        {
            int? result = _teamDbContext.RemoveMember(id);

            if (result == null)
                return NotFound(id);
            if (result == 0)
                return StatusCode(500, "Delete failed. Please try again.");
            return Ok();
        }

        //Sami - POST new data to the TeamMember table
        [HttpPost]
        public IActionResult PostMember(TeamMember teamMember)
        {
            int? result = _teamDbContext.AddMember(teamMember);

            if (result == null)
                return StatusCode(500, "Add failed. Team Member already exists.");
            if (result == 0)
                return StatusCode(500, "Add failed. Please ensure the id field is set to 0, then try again.");
            return Ok();
        }
    }
}
