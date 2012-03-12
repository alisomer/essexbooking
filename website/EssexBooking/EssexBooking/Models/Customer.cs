using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EssexBooking.Models
{
    public partial class Customer
    {
        public ASPNETDBEntities ctx;
        public void AddCustomer()
        {
            ctx.AddToCustomers(this);
            ctx.SaveChanges();
        }
    }
}