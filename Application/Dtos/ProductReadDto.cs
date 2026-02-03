namespace Amway.Application.Dtos
{
    public class ProductReadDto
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public string? BrandName { get; set; }
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Sku { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
