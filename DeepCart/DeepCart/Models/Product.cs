using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace DeepCart.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingAddress { get; set; }
        public int UnitPrice { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public byte[] ImageFile { get; set; }
        public string TC { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
    }
}