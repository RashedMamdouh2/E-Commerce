using E_Commerce.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;

public class OrderProduct : IEntity<int>
{
    public int Id { get; set; }
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

    

}

