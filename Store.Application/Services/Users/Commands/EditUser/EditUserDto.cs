namespace Store.Application.Services.Users.Commands.EditUser
{
    public class EditUserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }

    }

}

