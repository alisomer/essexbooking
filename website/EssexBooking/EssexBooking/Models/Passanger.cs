using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EssexBooking.Models
{
    [MetadataType(typeof(PassangerValidation))]
    public partial class Passanger
    {
        
    }

    public partial class PassangerValidation
    {
        [Required]
        [Display(Name = "First Name")]
        public string first_name{ get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Passaport No")]
        public string passaport_no { get; set; }
    }
}