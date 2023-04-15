using EntityFrameWork.Models;

namespace EntityFrameWork.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public int Count { get; set; }

     
        public  Product Product { get; set; }
    }
    }

