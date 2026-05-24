namespace MVC18.DTOs.Users.Customers
{
    public class CustomerDTO : UserBaseDTO
    {
        public string? CustomerCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? AvatarImage { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
