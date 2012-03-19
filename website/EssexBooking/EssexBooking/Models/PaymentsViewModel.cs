using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class PaymentsViewModel
    {
        public Cart Cart { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}