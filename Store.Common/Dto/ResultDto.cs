namespace Store.Common.Dto;

public class ResultDto : IConvertible
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
    public ResultDto(bool isSuccess, string message = "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public TypeCode GetTypeCode()
    {
        return TypeCode.Object;
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        return IsSuccess;
    }

    public byte ToByte(IFormatProvider? provider)
    {
        return ToByte(provider);
    }

    public char ToChar(IFormatProvider? provider)
    {
        return IsSuccess.ToString()[0];
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        return DateTime.Now;
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : 0;
    }

    public double ToDouble(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : 0;
    }

    public short ToInt16(IFormatProvider? provider)
    {
        return IsSuccess ? (short)1 : (short)0;
    }

    public int ToInt32(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : 0;
    }

    public long ToInt64(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : 0;
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        return IsSuccess ? (sbyte)1 : (sbyte)0;
    }

    public float ToSingle(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : 0;
    }

    public string ToString(IFormatProvider? provider)
    {
        return Message;
    }

    public virtual object? ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType.Name == nameof(ResultDto))
        {
            conversionType.GetProperty(nameof(Message)).SetValue(this, Message);
            conversionType.GetProperty(nameof(IsSuccess)).SetValue(this, IsSuccess);
            return this;
        }
        return null;
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        return IsSuccess ? (ushort)1 : (ushort)0;
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : (uint)0;
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        return IsSuccess ? 1 : (ulong)0;
    }
}
public class ResultDto<TKey> : ResultDto
{
    public TKey Data { get; set; }
    public ResultDto()
    {

    }
    public ResultDto(TKey data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }
    public ResultDto(string message = "خطا") : base(message)
    {

    }
    public override object? ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType.Name == typeof(ResultDto<TKey>).Name)
        {
            conversionType.GetProperty(nameof(Message)).SetValue(this, Message);
            conversionType.GetProperty(nameof(IsSuccess)).SetValue(this, IsSuccess);
            conversionType.GetProperty(nameof(Data)).SetValue(this, Data);
            return this;
        }
        return null;
    }
}
public class ResultDtoError : ResultDto // try-catch usage (for catch) 
{
    public new string Message { get; set; } = "خطایی رخ داد !";
}
