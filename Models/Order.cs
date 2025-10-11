using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Order:IEntity<int>
    {
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public decimal InvoiceValue { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProduct> Items { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
    public enum OrderStatus
    {
        Delivered,Canceled,Shipped
    }
 
}
