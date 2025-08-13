using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> repository;

        public CategoryController(IRepository<Category>repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowCategory(int id)
        {
            Category category = repository.GetById(id);
                //new Category
            //{

            //    Id = 1,
            //    Name="Food",
            //    Description="Food for Human per individual",
            //    Products=new List<Product> { 
            //        new Product{Id=1,Name="Pepsi",Description="FOOD",Images=new List<Image>{
            //            new Image{Url="Pepsi/1.webp" },
            //            new Image{Url="Pepsi/2.webp" },
            //            new Image{Url="Pepsi/3.webp" }
                    
            //        } },
            //        new Product{Id=2,Name="Chips",Description="FOOD",Images=new List<Image>{
            //            new Image{Url="Chepsi/1.jpg"},
            //            new Image{Url="Chepsi/2.jpg"},
            //            new Image{Url="Chepsi/3.jpg"}
                    
            //        }},
            //        new Product{Id=3,Name="Bescu",Description="FOOD",Images=new List<Image>{
            //            new Image{Url="Bescuit/1.jpg"},
            //            new Image{Url="Bescuit/2.jpg"},
            //            new Image{Url="Bescuit/3.jpg"}
                    
            //        }},
            //    }

            //};
            
            return View(category);
        }
    }
}
