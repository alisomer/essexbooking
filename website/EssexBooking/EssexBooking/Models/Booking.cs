using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;



namespace EssexBooking.Models
{
    public partial class Booking : EntityObject
    {
        public int temp_id { get; set; }
        public Dictionary<int, ExtraBooking> temp_extras { get; set; }
    }
}