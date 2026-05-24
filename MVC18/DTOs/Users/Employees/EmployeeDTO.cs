namespace MVC18.DTOs.Users.Employees
{
    public partial class EmployeeDTO : UserBaseDTO
    {
        public string DepartmentName { get; set; } = null!;

        public string PositionName { get; set; } = null!;

        public string EmployeeCode { get; set; } = null!;

        public DateOnly HiredDate { get; set; }

        public string? Status { get; set; }

        public string? AvatarImage { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
