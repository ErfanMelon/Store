namespace Store.Common;
/// <summary>
/// مدلی برای ارسال داده ها به صورت صفحه بندی
/// </summary>
/// <typeparam name="TItem">نوع داده</typeparam>
public class PaginationDto<TItem>
{
    public List<TItem> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int RowsCount { get; set; }
}
public static class Pagination
{
    public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
    {
        rowsCount = source.Count();
        return source.Skip((page - 1) * pageSize).Take(pageSize);
    }
    public static PaginationDto<TSource> ToPaged<TSource>(this IQueryable<TSource> source, int page, int pageSize)
    {
        PaginationDto<TSource> result = new PaginationDto<TSource>
        {
            Page = page,
            PageSize = pageSize,
            RowsCount= source.Count(),
            Items= source.Skip((page - 1) * pageSize).Take(pageSize).ToList()
        };
        return result;
    }

}
