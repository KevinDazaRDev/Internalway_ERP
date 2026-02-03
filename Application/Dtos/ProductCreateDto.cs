using System.ComponentModel.DataAnnotations;

namespace Amway.Application.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public long BrandId { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Slug { get; set; } = null!;

        [StringLength(100)]
        public string? Sku { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
