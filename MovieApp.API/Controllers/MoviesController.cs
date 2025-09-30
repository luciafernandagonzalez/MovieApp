using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ISynchronizationService _synchronizationService;

        public MoviesController(IMovieService movieService, ISynchronizationService synchronizationService)
        {
            _movieService = movieService;
            _synchronizationService = synchronizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieService.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Regular, Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _movieService.GetById(id);  
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MovieCreateRequestDto request)
        {
            var newMovie = await _movieService.Create(request);
            return StatusCode(201, newMovie);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] MovieUpdateRequestDto request)
        {
            var updated = await _movieService.Update(id, request);

            if (updated == null)
            {
                return NotFound($"Movie with ID {id} not found");
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.Delete(id);

            if (!result) return NotFound($"Movie with ID {id} not found");

            return NoContent();
        }
        [HttpPost("sync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SyncMovies()
        {
            await _synchronizationService.SynchronizeMovies();
            return Ok();
        }
    }
}
