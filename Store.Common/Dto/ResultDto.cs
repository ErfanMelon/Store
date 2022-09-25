namespace Store.Common.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
    }
    public class ResultDto<TKey>:ResultDto
    {
        public TKey Data { get; set; } 
    }
    public class ResultDtoError:ResultDto // try-catch usage (for catch) 
    {
        public string Message { get; set; } = "خطایی رخ داد !";
    }
}
