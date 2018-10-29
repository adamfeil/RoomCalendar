using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace RoomCalendars.Controllers





{

    public class EventDTO
    {
        public DateTime begin { get; set; }
        public DateTime end { get; set; }
        public string summary { get; set; }
        public string organizer { get; set; }
    }





    public class CalDataController : ApiController
    {

        public List<EventDTO> getEvents(string calendarURL)
        {

            var client = new WebClient();
            var calData = client.DownloadString(calendarURL);

            var c = Calendar.Load(calData);

            var occ = c.GetOccurrences(DateTime.Now, DateTime.Now.AddDays(7)).OrderBy(r => r.Period.StartTime.Value.ToUniversalTime()).ToList();

            occ = occ.Where(o => o.Period.EndTime.Value.ToUniversalTime() > DateTime.Now.ToUniversalTime()).ToList();

             var returnEvents = new List<EventDTO>();

            foreach (var o in occ)
            {
                if (o.Source.GetType().Name == "CalendarEvent")
                {
                    var e = new EventDTO();
                    e.begin = o.Period.StartTime.Value;
                    e.end = o.Period.EndTime.Value;
                    var x = (CalendarEvent)o.Source;
                    if(x.Organizer != null)
                    {
                        e.organizer = x.Organizer.CommonName;
                    }
                    else
                    {
                        e.organizer = "";
                    }
                    
                    e.summary = x.Summary;

                    returnEvents.Add(e);
                    
                }
               
                

            }

            

           
          
            
            return returnEvents;


        }

    }
}
