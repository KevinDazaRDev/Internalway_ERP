using System.ComponentModel.DataAnnotations;

namespace Amway.Application.Dtos
{
    public class BrandUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Slug { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
