using Moq;
using MovieApp.Application.Interfaces;
using MovieApp.Domain;
using MovieApp.Infrastructure.Services;

namespace MovieApp.Tests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllMovies()
        {
            //crear mock
            var mockMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie A"},
                new Movie { Id = 2, Title = "Movie B" }
            };

            var mockRepo = new Mock<IMovieRepository>();

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(mockMovies);
            var service = new MovieService(mockRepo.Object);

            //act
            var result = await service.GetAll();

            //assert
            Assert.Equal(2, result.Count);
            mockRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
