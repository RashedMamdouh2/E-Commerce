using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly IRepository<Category,CategoryViewModel> repository;

        public CategoriesViewComponent(IRepository<Category, CategoryViewModel> repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {


            var categories = repository.GetAllًWithNameAndIdOnly();
            //    new List<Category> {
            //    new Category { Id=1,Name="Phones",Description="Buy New Phones"} ,
            //    new Category { Id=1,Name="TVs",Description="Buy New Phones"} ,
            //    new Category { Id=1,Name="Electronics",Description="Buy New Phones"} ,
            //    new Category { Id=1,Name="PC Accessories",Description="Buy New Phones"} ,
            //    new Category { Id=1,Name="TX",Description="Buy New Phones"} ,
            //    new Category { Id=1,Name="r2",Description="Buy New Phones"}, 
            //    new Category { Id=1,Name="College",Description="Buy New Phones"}, 
            //    new Category { Id=1,Name="Univirsisty",Description="Buy New Phones"}, 
            //    new Category { Id=1,Name="MAC Book",Description="Buy New Phones"}, 
            //    new Category { Id=1,Name="Ezyk",Description="Buy New Phones"} 
            //};
            
            return View("Categories-Nav",categories);
        }
        
    }
}
