using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class Test : Controller
    {
        [Route("Rashed/{id:int}")]
        public IActionResult Act1(int id,string name)
        {
            
            string s = "لا اله الا الله محمد رسول الله ";
            for (int i = 0; i < id; i++)
            {
                s += "\n";
                s += s;
            }
            s += "\n" + name;
            return Content(s);
        }
    }
}
