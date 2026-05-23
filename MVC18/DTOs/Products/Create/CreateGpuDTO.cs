namespace MVC18.DTOs.Products.Create
{
    public class CreateGpuDTO : CreateProductDTO
    {
        public float MemorySize { get; set; }

        public string MemoryType { get; set; } = null!;

        public int Clock { get; set; }

        public int UnifiedShader { get; set; }

        public int Tmu { get; set; }

        public int Rop { get; set; }

        public string Bus { get; set; } = null!;

        public bool? Igpu { get; set; }
    }
}
