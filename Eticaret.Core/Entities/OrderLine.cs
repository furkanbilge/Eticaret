using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class OrderLine : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Sipariş Numarası")]
        public int OrderId { get; set; }
        [Display(Name = "Sipariş")]
        public Order? order { get; set; }
        [Display(Name = "Ürün Numarası")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün")]
        public Product? Product { get; set; }
        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
        [Display(Name = "Birim Fiyatı")]
        public decimal UnitPrice { get; set; }
    }
}
