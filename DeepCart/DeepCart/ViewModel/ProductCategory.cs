using DeepCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeepCart.ViewModel
{
    public class ProductCategory
    {
        public string Category{ get; set; }
        public List<Product> Products { get; set; }

    }
}