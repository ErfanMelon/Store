namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUserDto
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
