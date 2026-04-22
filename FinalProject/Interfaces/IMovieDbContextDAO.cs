using FinalProject.Models;

namespace FinalProject.Interfaces
{
    public interface IMovieDbContextDAO
    {
        List<Movie> GetAllRecords();
        Movie GetRecordById(int? id);
        int? UpdateRecord(Movie movie);
        int? RemoveRecord(int id);
        int? AddRecord(Movie movie);
    }
}
