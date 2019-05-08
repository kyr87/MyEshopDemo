using MyEshopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Viewmodels
{
    public class OrderDetailsViewModel
    {
        public ApplicationUser User { get; set; }

        public Dictionary<Product,int> ProductsWithTheirQuantities { get; set; }

        private decimal grandTotal;

        public decimal GrandTotal
        {
            get
            {
                foreach(var item in ProductsWithTheirQuantities)
                {
                    grandTotal += item.Key.Price * item.Value;
                }
                return grandTotal;
            }
        }


    }
}