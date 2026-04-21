namespace FinalProject.Models
{
    public class Hobby
    {
        public string HobbyName { get; set; }
        //Sami - updated Id type from string to int and re-migrated db
        public int Id { get; set; }
        public string Category { get; set; }
        public int HoursPerWeek { get; set; }
        public string Difficulty { get; set; }

    }
}
