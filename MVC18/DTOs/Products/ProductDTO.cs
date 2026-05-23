namespace MVC18.DTOs.Products
{
    public class ProductDTO
    {
        public Guid ProductUuid { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public string CategoryName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
