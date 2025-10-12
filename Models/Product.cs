using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime? InsertionDate { get; set; }

        public List<Feedback> Feedbacks { get; set; }
        public List<Image> Images { get; set; }
        public List<Filter> Filters { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Customer> Customers { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}