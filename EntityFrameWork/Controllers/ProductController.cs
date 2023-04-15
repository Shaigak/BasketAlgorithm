using EntityFrameWork.Data;
using EntityFrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Controllers
{
    public class ProductController : Controller

    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _context.Products.Include(m => m.Images).Where(m => !m.SoftDelete).Take(4).ToList();
            return View(products);
        }


     
    }
}
