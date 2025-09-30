using System.ComponentModel.DataAnnotations;

namespace MovieApp.Application.DTOs
{
    public class MovieCreateRequestDto
    {
        [Required] 
        public string Title { get; set; }

        [Required] 
        public string Director { get; set; }
        [Required]
        public string Producer { get; set; }
    }
}

