using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EssexBooking.Models
{
    public class TelesalesViewModel
    {
        //  public EntitiesList EntitiesList { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string DateOfTravel { get; set; }
        public string NumberOfGuests { get; set; }

        public string NumberOfNights { get; set; }

        public List<Resort> Resorts;
        public List<HotelType> HotelTypes;
        public List<Extra> Extras;
        public List<TravelType> TravelTypes;

        public TelesalesViewModel()
        {

            EntitiesRepository Repositories = new EntitiesRepository();
            Resorts = Repositories.GetResorts().ToList();
            HotelTypes = Repositories.GetHotelTypes().ToList();
            Extras = Repositories.GetExtras().ToList();
            TravelTypes = Repositories.GetTravelTypes().ToList();
        }
    }
}