namespace Store.EndPoint.Models
{
    public class CommentViewModel
    {
        public long ProductId { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
        public int Stars { get; set; }
        public List<string> Pros { get; set; }
        public List<string> Cons { get; set; }
    }
}
