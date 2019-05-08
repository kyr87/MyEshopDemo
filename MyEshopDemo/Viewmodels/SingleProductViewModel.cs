using MyEshopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Viewmodels
{
    public class SingleProductViewModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}