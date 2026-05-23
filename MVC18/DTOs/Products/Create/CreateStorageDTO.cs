namespace MVC18.DTOs.Products.Create
{
    public class CreateStorageDTO : CreateProductDTO
    {
        public int Capacity { get; set; }

        public string MemoryType { get; set; } = null!;

        public string InterfaceType { get; set; } = null!;

        public int ReadSpeed { get; set; }

        public int WriteSpeed { get; set; }
    }
}
