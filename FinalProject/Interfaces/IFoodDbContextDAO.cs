using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface IFoodDbContextDAO
    {
        List<Food> GetAllRecords();
        Food GetRecordById(int? id);
        int? UpdateRecord(Food food);
        int? RemoveRecord(int id);
        int? AddRecord(Food food);
    }
}
