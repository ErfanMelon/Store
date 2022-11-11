using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities.Orders
{
    /// <summary>
    /// وضعیت سفارش
    /// </summary>
    public enum OrderState:int
    {
        [Display(Name = "درحال پردازش")]
        InProccess=0,
        [Display(Name = "درحال ارسال")]
        Sending = 1,
        [Display(Name = "لغو شده")]
        Cancelled=2,
        [Display(Name = "تحویل داده شده")]
        Delivered=3,
    }
}
