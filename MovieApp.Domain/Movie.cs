namespace MovieApp.Domain
{
    public class Movie
    {
        public int Id { get; set; } //pk de bd
        public int EpisodeId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
    }
}