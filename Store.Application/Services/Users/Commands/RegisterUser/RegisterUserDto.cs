namespace Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
