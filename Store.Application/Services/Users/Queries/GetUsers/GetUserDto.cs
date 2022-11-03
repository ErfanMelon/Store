namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUserDto
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public int UserRole { get; set; }
        public string UserRoleName { get; set; }
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ZipCode { get; set; }

    }
}
