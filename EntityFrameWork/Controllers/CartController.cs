using EntityFrameWork.Data;
using EntityFrameWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EntityFrameWork.Controllers
{
    public class CartController : Controller
    {


        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {

            List<BasketVM> basketProducts= JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            List<BasketDetailVM> basketDetails = new();

            foreach (var product in basketProducts)
            {
                var dbProduct = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == product.Id);


                basketDetails.Add(new BasketDetailVM
                {
                    Id = dbProduct.Id,
                    Name=dbProduct.Name,
                    Description=dbProduct.Description,
                    Price=dbProduct.Price,
                    Count=product.Count,
                    Image=dbProduct.Images.Where(m=>m.IsMain).FirstOrDefault().Image,
                    Total=product.Count*dbProduct.Price

                });
            }
             
            return View(basketDetails);
        }

        [ActionName("Delete")]
         public IActionResult DeleteProductFromBasket(int ? id)
        {

            if (id is null) return BadRequest();

            List<BasketVM> basketProducts= JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

            BasketVM deletedProduct = basketProducts.FirstOrDefault(m => m.Id == id);

            basketProducts.Remove(deletedProduct);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
            
            return RedirectToAction(nameof(Index));
        }


    }

}
