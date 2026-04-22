using FinalProject.Interfaces;
using FinalProject.Models;

namespace FinalProject.Data
{
    public class FoodDbContextDAO : IFoodDbContextDAO
    {
        //Sami - inject DbContext
        private readonly AppDbContext _daoContext;
        public FoodDbContextDAO(AppDbContext daoContext)
        {
            _daoContext = daoContext;
        }

        //Sami - build DAO for GET ALL
        public List<Food> GetAllRecords()
        {
            return _daoContext.Foods.ToList();
        }

        //Sami - build DAO for GET BY ID
        public Food GetRecordById(int? id)
        {
            return _daoContext.Foods.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        //Sami - build DAO for PUT
        public int? UpdateRecord(Food food)
        {
            var foodToUpdate = this.GetRecordById(food.Id);
            if (foodToUpdate == null) return null;
            try
            {
                foodToUpdate.Name = food.Name;
                foodToUpdate.Calories = food.Calories;
                foodToUpdate.CuisineType = food.CuisineType;
                foodToUpdate.MealType = food.MealType;
                _daoContext.Foods.Update(foodToUpdate);
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
            var foodToRemove = this.GetRecordById(id);
            if (foodToRemove == null) return null;
            try
            {
                _daoContext.Foods.Remove(foodToRemove);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for POST
        public int? AddRecord(Food food)
        {
            var foodToAdd = _daoContext.Foods.
                Where(x => x.Name.Equals(food.Name)
                && x.CuisineType.Equals(food.CuisineType)
                && x.MealType.Equals(food.MealType)).FirstOrDefault();
            if (foodToAdd != null) return null;
            try
            {
                _daoContext.Foods.Add(food);
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
