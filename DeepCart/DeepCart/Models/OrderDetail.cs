using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeepCart.Models
{
   
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int SellerID { get; set; }
        public string ProductName { get; set; }
        public int OrderedQuantity { get; set; }
        public int PerUnitPrice { get; set; }
        [ForeignKey("OrderD")]
        public int OrderID { get; set; }

        public virtual OrderDetail OrderD { get; set; }
    }
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public string IP { get; set; }
        public string OrderPayMethod { get; set; }
        public string PaymentRefrenceId { get; set; }
        public bool IsConfirmed { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}