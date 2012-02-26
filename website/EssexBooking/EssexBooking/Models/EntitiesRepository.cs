using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class EntitiesRepository
    {
        private ASPNETDBEntities entities = new ASPNETDBEntities();

        public IQueryable<Resort> GetResorts()
        {
            return entities.Resorts;
        }
        public IQueryable<HotelType> GetHotelTypes()
        {
            return entities.HotelTypes;
        }
        public IQueryable<TravelType> GetTravelTypes()
        {
            return entities.TravelTypes;
        }
        public IQueryable<Extra> GetExtras()
        {
            return entities.Extras;
        }

    }
}