
namespace Store.EndPoint.Tools
{
    public interface ICookieManager
    {
        /// <summary>
        /// این تابع برای افزودن کوکی استفاده میشود
        /// </summary>
        /// <param name="context">ارتباط</param>
        /// <param name="token">نام کلید کوکی</param>
        /// <param name="value">مقدار کوکی</param>
        void Add(HttpContext context, string token, string value);
        /// <summary>
        /// این تابع برای چک کردن وجود کلید در کوکی به کار میرود
        /// </summary>
        /// <param name="context">ارتباط</param>
        /// <param name="token">نام کلید کوکی</param>
        /// <returns></returns>
        bool Contains(HttpContext context, string token);
        /// <summary>
        /// مقدار کوکی را دریافت میکند
        /// </summary>
        /// <param name="context">ارتباط</param>
        /// <param name="token">نام کلید کوکی</param>
        /// <returns></returns>
        string GetValue(HttpContext context, string token);
        /// <summary>
        /// کوکی را از مرورگر کاربر حذف میکند
        /// </summary>
        /// <param name="context">ارتباط</param>
        /// <param name="token">نام کلید کوکی</param>
        void Remove(HttpContext context, string token);
        /// <summary>
        /// این تابع کلید مرورگر کاربر را برمیگرداند
        /// </summary>
        /// <param name="context">ارتباط</param>
        /// <returns>کلید مرورگر کاربر</returns>
        Guid GetBrowserId(HttpContext context);
    }
    public class CookieManager : ICookieManager
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

                public string GetValue(HttpContext context, string token)
        {
            if (context.Request.Cookies.TryGetValue(token, out string value))
            {
                return value;
            }
            return null;
        }

        public void Remove(HttpContext context, string token)
        {
            if (Contains(context,token))
            {
                context.Response.Cookies.Delete(token);
            }
        }

        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100),
            };
        }
        public Guid GetBrowserId(HttpContext context)
        {
            Guid result;
            string token = "BrowserId";
            string browserid = GetValue(context, token);
            if (string.IsNullOrEmpty(browserid))
            {
                result = Guid.NewGuid();
                Add(context, token, result.ToString());
            }
            else
                Guid.TryParse(browserid, out result);
            return result;
        }
    }
}
