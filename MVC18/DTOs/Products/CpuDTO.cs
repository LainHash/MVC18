namespace MVC18.DTOs.Products
{
    public class CpuDTO
    {
        public string CategoryName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public Guid ProductUuid { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int Cores { get; set; }

        public int Logicals { get; set; }

        public float Tdp { get; set; }

        public string Socket { get; set; } = null!;

        public int Speed { get; set; }

        public int Turbo { get; set; }
    }
}
