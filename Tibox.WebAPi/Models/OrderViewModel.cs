using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tibox.Models;

namespace Tibox.WebAPi.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public IList<OrderItem> OrderItems { get; set; } 
    }
}