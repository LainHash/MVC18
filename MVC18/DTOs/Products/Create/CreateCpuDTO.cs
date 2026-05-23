namespace MVC18.DTOs.Products.Create
{
    public class CreateCpuDTO : CreateProductDTO
    {
        public int Cores { get; set; }

        public int Logicals { get; set; }

        public float Tdp { get; set; }

        public string Socket { get; set; } = null!;

        public int Speed { get; set; }

        public int Turbo { get; set; }
    }
}
