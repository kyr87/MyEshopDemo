﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}