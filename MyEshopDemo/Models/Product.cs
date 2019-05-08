using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshopDemo.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool Featured { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Thumbnail { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        // Foreign Key
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}