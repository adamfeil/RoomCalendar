using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ical.Net;

namespace RoomCalendars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var client = new WebClient();
            var calData = client.DownloadString("https://calendar.google.com/calendar/ical/graphicsland.com_3830353733323030333330%40resource.calendar.google.com/private-33cf26ee7eca3c690862dc30e808a12d/basic.ics");

           var c =  Calendar.Load(calData);

            var events = c.Events.ToList();



            return View(events);
        }
    }
}
