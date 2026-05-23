namespace MVC18.DTOs.Products.Create
{
    public class CreateRamDTO : CreateProductDTO
    {
        public int Capacity { get; set; }

        public string Gen { get; set; } = null!;

        public int Speed { get; set; }

        public string Kit { get; set; } = null!;
    }
}
