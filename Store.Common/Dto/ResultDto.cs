namespace Store.Common.Dto;

public class ResultDto
{
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; }
    public ResultDto()
    {

    }
    public ResultDto(string message = "خطا")
    {
        Message = message;
    }
    public ResultDto(bool isSuccess,string message="")
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}
public class ResultDto<TKey>:ResultDto
{
    public TKey Data { get; set; }
    public ResultDto()
    {

    }
    public ResultDto(TKey data,bool isSuccess=true, string message = ""):base(isSuccess,message)
    {
        Data = data;
    }
    public ResultDto(string message = "خطا"):base(message)
    {

    }
}
public class ResultDtoError:ResultDto // try-catch usage (for catch) 
{
    public string Message { get; set; } = "خطایی رخ داد !";
}
