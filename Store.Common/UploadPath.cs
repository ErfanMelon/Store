namespace Store.Common;

public class UploadPath
{
    const string rootFolderName = "Library";
    /// <summary>
    /// مشخص کردن مسیر ذخیره فایل در سرور
    /// </summary>
    /// <param name="fileType">نوع فایل</param>
    /// <returns></returns>
    public static string Get(UploadFileType fileType)
    {
        switch (fileType)
        {
            case UploadFileType.ProductImage:
                return $@"images\ProductImages\";
            case UploadFileType.BannerImage:
                return $@"images\BannerImages\";
            default:
                return $@"images\";
        }
    }
    public enum UploadFileType
    {
        ProductImage,
        BannerImage
    }
}
