namespace Amway.Models
{
    public class Product
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Sku { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Brand Brand { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
