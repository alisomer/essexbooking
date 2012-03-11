using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class TeleSalesPanelViewModel
    {
        public Customer customer { get; set; }
        public TeleSalesPanelViewModel()
        {
            customer = (Customer)System.Web.HttpContext.Current.Session["customer"];
            Cart cart = new Cart();
        }


    }
}