using FinalProject.Models;

namespace FinalProject.Interfaces
{
    //Sami - initialize new interface for accessing data
    public interface ITeamDbContextDAO
    {
        List<TeamMember> GetAllMembers();
        TeamMember GetMember(int id);
        int? UpdateMember(TeamMember teamMember);
        int? RemoveMember(int id);
        int? AddMember(TeamMember teamMember);
    }
}
