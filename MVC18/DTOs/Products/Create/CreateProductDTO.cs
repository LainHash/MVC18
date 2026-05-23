namespace MVC18.DTOs.Products.Create
{
    public class CreateProductDTO
    {
        public int CategoryId { get; set; } 

        public int CompanyId { get; set; } 

        public string ProductName { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}
