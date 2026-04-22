using System.Collections.Generic;
using FinalProject.Interfaces;
using FinalProject.Models;

namespace FinalProject.Data
{
    //Sami - build data access objects
    public class TeamDbContextDAO : ITeamDbContextDAO
    {
        //Sami - inject DbContext
        private readonly AppDbContext _daoContext;
        public TeamDbContextDAO(AppDbContext daoContext)
        {
           _daoContext = daoContext;
        }

        //Sami - build DAO for GET ALL
        public List<TeamMember> GetAllMembers()
        {
            return _daoContext.TeamMembers.ToList();
        }

        //Sami - build DAO for GET BY ID
        public TeamMember GetMember(int? id)
        {
            return _daoContext.TeamMembers.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        //Sami - build DAO for PUT
        public int? UpdateMember(TeamMember teamMember)
        {
            var memberToUpdate = this.GetMember(teamMember.Id);
            if (memberToUpdate == null) return null;
            try
            {
                memberToUpdate.FullName = teamMember.FullName;
                memberToUpdate.BirthDate = teamMember.BirthDate;
                memberToUpdate.CollegeProgram = teamMember.CollegeProgram;
                memberToUpdate.YearInProgram = teamMember.YearInProgram;
                memberToUpdate.Email = teamMember.Email;
                _daoContext.TeamMembers.Update(memberToUpdate);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for DELETE BY ID
        public int? RemoveMember(int id)
        {
            var teamMember = this.GetMember(id);
            if (teamMember == null) return null;
            try
            {
                _daoContext.TeamMembers.Remove(teamMember);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for POST
        public int? AddMember(TeamMember teamMember)
        {
            var memberToAdd = _daoContext.TeamMembers.
                Where(x => x.FullName.Equals(teamMember.FullName)
                && x.BirthDate.Equals(teamMember.BirthDate)
                && x.Email.Equals(teamMember.Email)).FirstOrDefault();
            if (memberToAdd != null) return null;
            try
            {                
                _daoContext.TeamMembers.Add(teamMember);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
