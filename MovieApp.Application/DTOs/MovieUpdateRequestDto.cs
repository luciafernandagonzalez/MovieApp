using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.DTOs
{
    public class MovieUpdateRequestDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Producer { get; set; }
    }
}
