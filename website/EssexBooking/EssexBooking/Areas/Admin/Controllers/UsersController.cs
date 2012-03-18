using System.Web.Mvc;
using EssexBooking.Areas.Admin.ViewModels;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public ASPNETDBEntities entities = new ASPNETDBEntities();

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            return View(new UserViewModel(entities.aspnet_Users, entities.aspnet_Roles));
        }
    }
}
