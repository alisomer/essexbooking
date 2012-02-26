using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class HistoryModel
    {
        public string UserID { get; set; }
        public string email { get; set; }
        public Booking booking { get; set; }

        public void history(Guid userId)
        {
            ASPNETDBEntities db = new ASPNETDBEntities();
            var query = db.Bookings.SingleOrDefault(x => x.customer_id == userId);
        }
    }
}