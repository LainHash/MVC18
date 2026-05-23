namespace MVC18.DTOs.Products
{
    public class LaptopDTO
    {
        public string LaptopType { get; set; } = null!;

        public string Os { get; set; } = null!;

        public string ScreenResolution { get; set; } = null!;

        public float Length { get; set; }

        public float Weight { get; set; }

        public int Cores { get; set; }

        public int Logicals { get; set; }

        public float Tdp { get; set; }

        public string Socket { get; set; } = null!;

        public int CpuSpeed { get; set; }

        public int Turbo { get; set; }

        public float MemorySize { get; set; }

        public string MemoryType { get; set; } = null!;

        public int Clock { get; set; }

        public int UnifiedShader { get; set; }

        public int Tmu { get; set; }

        public int Rop { get; set; }

        public string Bus { get; set; } = null!;

        public int RamCapacity { get; set; }

        public string Gen { get; set; } = null!;

        public int RamSpeed { get; set; }

        public string Kit { get; set; } = null!;

        public int StorageCapacity { get; set; }

        public string StorageType { get; set; } = null!;

        public string InterfaceType { get; set; } = null!;

        public int ReadSpeed { get; set; }

        public int WriteSpeed { get; set; }

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
    }
}
