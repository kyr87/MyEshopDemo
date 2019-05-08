using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}