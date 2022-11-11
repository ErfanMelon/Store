namespace Store.Application.Services.Users.Commands.LoginUser
{
    public class UserLoginDto
    {
        public long UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }
}
