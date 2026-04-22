using FinalProject.Interfaces;
using FinalProject.Models;

namespace FinalProject.Data
{
    public class MovieDbContextDAO : IMovieDbContextDAO
    {
        //Sami - inject DbContext
        private readonly AppDbContext _daoContext;
        public MovieDbContextDAO(AppDbContext daoContext)
        {
            _daoContext = daoContext;
        }

        //Sami - build DAO for GET ALL
        public List<Movie> GetAllRecords()
        {
            return _daoContext.Movies.ToList();
        }

        //Sami - build DAO for GET BY ID
        public Movie GetRecordById(int? id)
        {
            return _daoContext.Movies.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        //Sami - build DAO for PUT
        public int? UpdateRecord(Movie movie)
        {
            var movieToUpdate = this.GetRecordById(movie.Id);
            if (movieToUpdate == null) return null;
            try
            {
                movieToUpdate.Title = movie.Title;
                movieToUpdate.Genre = movie.Genre;
                movieToUpdate.YearReleased = movie.YearReleased;
                movieToUpdate.rating = movie.rating;
                _daoContext.Movies.Update(movieToUpdate);
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
            var movieToRemove = this.GetRecordById(id);
            if (movieToRemove == null) return null;
            try
            {
                _daoContext.Movies.Remove(movieToRemove);
                _daoContext.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Sami - build DAO for POST
        public int? AddRecord(Movie movie)
        {
            var movieToAdd = _daoContext.Movies.
                Where(x => x.Title.Equals(movie.Title)).FirstOrDefault();
            if (movieToAdd != null) return null;
            try
            {
                _daoContext.Movies.Add(movie);
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
