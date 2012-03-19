using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class CreditCard
    {
        [Required]
        [Display(Name = "Credit card number")]
        public string Number { get; set; }

        [Required]
        public string CCV { get; set; }

        [Required]
        [Range(1,12)]
        [Display(Name = "Expiration month")]
        public int ExpMonth { get; set; }

        [Required]
        [Display(Name = "Expiration year")]
        public int ExpYear { get; set; }


        public bool Charge(decimal amount)
        {
            //Not implemented for the purposes of this assignment
            return true;
        }

        public bool isValid()
        {
            //Not implemented for the purposes of this assignment
            return true;
        }

        public bool hasAmount(decimal amount)
        {
            //Not implemented for the purposes of this assignment
            return true;
        }
    }

}