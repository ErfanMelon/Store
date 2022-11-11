namespace Store.EndPoint.Models.AuthenticationViewModel
{
    public class SignupViewModel
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
    }
}
