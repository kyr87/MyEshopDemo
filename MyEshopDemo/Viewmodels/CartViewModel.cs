using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Viewmodels
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }
        public decimal GrandTotal { get; set; }
    }
}