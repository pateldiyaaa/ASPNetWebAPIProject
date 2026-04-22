using FinalProject.Interfaces;
using FinalProject.Models;

namespace FinalProject.Data
{
    public class HobbyDbContextDAO : IHobbyDbContextDAO
    {
        //Sami - inject DbContext
        private readonly AppDbContext _daoContext;
        public HobbyDbContextDAO(AppDbContext daoContext)
        {
            _daoContext = daoContext;
        }

        //Sami - build DAO for GET ALL
        public List<Hobby> GetAllRecords()
        {
            return _daoContext.Hobbies.ToList();
        }

        //Sami - build DAO for GET BY ID
        public Hobby GetRecordById(int? id)
        {
            return _daoContext.Hobbies.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        //Sami - build DAO for PUT
        public int? UpdateRecord(Hobby hobby)
        {
            var hobbyToUpdate = this.GetRecordById(hobby.Id);            
            if (hobbyToUpdate == null) return null;
            try
            {
                hobbyToUpdate.HobbyName = hobby.HobbyName;
                hobbyToUpdate.Category = hobby.Category;
                hobbyToUpdate.HoursPerWeek = hobby.HoursPerWeek;
                hobbyToUpdate.Difficulty = hobby.Difficulty;
                _daoContext.Hobbies.Update(hobbyToUpdate);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for DELETE BY ID
        public int? RemoveRecord(int id)
        {
            var hobbyToRemove = this.GetRecordById(id);
            if (hobbyToRemove == null) return null;
            try
            {
                _daoContext.Hobbies.Remove(hobbyToRemove);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for POST
        public int? AddRecord(Hobby hobby)
        {
            var hobbyToAdd = _daoContext.Hobbies.
                Where(x => x.HobbyName.Equals(hobby.HobbyName)).FirstOrDefault();
            if (hobbyToAdd != null) return null;
            try
            {
                _daoContext.Hobbies.Add(hobby);
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
