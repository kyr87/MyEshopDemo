using MyEshopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Viewmodels
{
    public class SingleCategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}