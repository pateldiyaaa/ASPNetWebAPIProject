using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface IHobbyDbContextDAO
    {
        List<Hobby> GetAllRecords();
        Hobby GetRecordById(int? id);
        int? UpdateRecord(Hobby hobby);
        int? RemoveRecord(int id);
        int? AddRecord(Hobby hobby);
    }
}
