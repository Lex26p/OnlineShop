using System.ComponentModel.DataAnnotations;

namespace Ozon.Db.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        OnTheWay,
        [Display(Name = "Отменен")]
        Canceled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
