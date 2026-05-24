namespace MVC18.DTOs.Users
{
    public class UserBaseDTO
    {
        public Guid UserUuid { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal Balance { get; set; }

        public string RoleName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool Gender { get; set; }

        public DateOnly Dob { get; set; }

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string CitizenIdentityCard { get; set; } = null!;
    }
}
