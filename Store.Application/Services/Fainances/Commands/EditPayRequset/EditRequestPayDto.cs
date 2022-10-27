namespace Store.Application.Services.Fainances.Commands.EditPayRequset
{
    public class EditRequestPayDto
    {
        public bool IsPay { get; set; }
        public Guid RequsetPayId { get; set; }
        public string Description { get; set; }
        public int RefId { get; set; }
        public string Authority { get; set; }
    }
}
