﻿namespace EntityFrameWork.ViewModels
{
    public class BasketDetailVM
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Id { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }

        public string? Image { get; set; }

        public decimal Total { get; set; }

        public decimal GrandTotal { get; set; }

    }
}
