using System;

namespace ShareMyThings.Models.Use
{
    public class Ok
    {
        public int ItemId { get; set; }

        public string UserName { get; set; }

        public string ItemName { get; set; }

        public DateTime ReservationEnd { get; set; }

        public DateTime ReservationSlackEnd { get; set; }

        public bool ShowNextReservation { get; set; }

        public DateTime NextReservationSlackStart { get; set; }

        public DateTime NextReservationStart { get; set; }

        public string NextUserName { get; set; }

        public string NextUserPhone { get; set; }

        public DateTime Now { get; set; }
    }
}