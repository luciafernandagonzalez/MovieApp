using MovieApp.Application.Interfaces;
using MovieApp.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieApp.Infrastructure.Services
   
{   public class SwapMoviesResponse 
    {
        [JsonPropertyName("result")]
        public List<SwapMovieResult> Result { get; set; }
    }
    public class SwapMovieResult
    {
        [JsonPropertyName("properties")]
        public MovieProperties Properties { get; set; }
    }
    public class SwapiResponse
    {
        [JsonPropertyName("result")]
        public SwapiResult Result { get; set; }
    }
    public class SwapiResult
    {
        [JsonPropertyName("properties")]
        public MovieProperties Properties { get; set; }
    }
    public class MovieProperties
    {
        [JsonPropertyName("episode_id")]
        public int EpisodeId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("director")]
        public string Director { get; set; }
        [JsonPropertyName("producer")]
        public string Producer { get; set; }
    }

    public class StarWarsApiService : IStarWarsApiService
    {
        private readonly HttpClient _httpClient;
        public StarWarsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var response = await _httpClient.GetAsync($"https://www.swapi.tech/api/films/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var swapiResponse = JsonSerializer.Deserialize<SwapMoviesResponse>(content);

            var movies = new List<Movie>();
            if (swapiResponse?.Result != null)
            {
                foreach(var result in swapiResponse.Result)
                {
                    var apiMovie = result.Properties;
                    movies.Add(new Movie
                    {
                        EpisodeId = apiMovie.EpisodeId,
                        Title = apiMovie.Title,
                        Director = apiMovie.Director,
                        Producer = apiMovie.Producer,
                    });
                }
            }
            return movies;
        }

        public async Task<Movie> GetMovie(int filmId)
        {
            var response = await _httpClient.GetAsync($"https://www.swapi.tech/api/films/{filmId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var swapiResponse = JsonSerializer.Deserialize<SwapiResponse>(content);

            // Mapeo del DTO al objeto de dominio Movie
            if (swapiResponse?.Result?.Properties != null)
            {
                var apiMovie = swapiResponse.Result.Properties;
                return new Movie
                {
                    Title = apiMovie.Title,
                    Director = apiMovie.Director,
                    Producer = apiMovie.Producer
                };
            }
            return null;
        }
    }
}
