using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EssexBooking.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string PassportNumber { get; set; }
    }
}