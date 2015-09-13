using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.ViewModel.Use
{
    public class OkViewModelRow
    {
        public string Time { get; set; }

        public string Due { get; set; }

        /// <summary>
        /// Class name on Due field.
        /// </summary>
        public string Alert { get; set; }
    }


    public class OkViewModel : BaseViewModel
    {
        public int ItemId { get; set; }

        public OkViewModelRow ReservationEnd { get; set; }

        public OkViewModelRow ReservationSlackEnd { get; set; }


        public bool ShowNextReservation { get; set; }

        public OkViewModelRow NextReservationSlackStart { get; set; }

        public OkViewModelRow NextReservationStart { get; set; }

        public string NextReservationDetails { get; set; }
    }
}