﻿using System.Runtime.CompilerServices;

namespace Google_Calender_Reminder.Models
{
    public class Event
    {
        public Event()
        {
            this.Start = new EventDateTime()
            {
                TimeZone = "Asia/Kolkata"
            };
            this.End = new EventDateTime()
            {
                TimeZone = "Asia/Kolkata"
            };

        }
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
    }
    public class EventDateTime
    {
        public string DateTime { get; set; }
        public string TimeZone { get; set; }
    }

}


