using MovieApp.Application.Interfaces;

namespace MovieApp.Infrastructure.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IStarWarsApiService _starWarsApiService;
        private readonly IMovieRepository _movieRepository;

        public SynchronizationService(IStarWarsApiService starWarsApiService, IMovieRepository movieRepository)
        {
            _starWarsApiService = starWarsApiService;
            _movieRepository = movieRepository;
        }

        public async Task SynchronizeMovies()
        {
            var apiMovies = await _starWarsApiService.GetAllMovies();
            var dbMovies = await _movieRepository.GetAll();

            foreach (var movie in apiMovies)
            {
                var existingMovie = dbMovies.FirstOrDefault(db => db.EpisodeId == movie.EpisodeId);

                if (existingMovie == null)
                {
                    await _movieRepository.Add(movie);
                }
                else
                {
                    existingMovie.Title = movie.Title;  
                    existingMovie.Director = movie.Director;
                    existingMovie.Producer = movie.Producer;
                    
                    await _movieRepository.Update(existingMovie);
                }
            }
        }
    }
}
