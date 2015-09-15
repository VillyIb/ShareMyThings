using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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

        public string Now { get; set; }

        // -- current reservation

        public OkViewModelRow ReservationEnd { get; set; }

        public OkViewModelRow ReservationSlackEnd { get; set; }

        // --- next reservation 

        public bool ShowNextReservation { get; set; }

        public OkViewModelRow NextReservationSlackStart { get; set; }

        public OkViewModelRow NextReservationStart { get; set; }

        /// <summary>
        /// Current and next reservation has overlapping slack.
        /// </summary>
        public bool HasOverlap { get; set; }

        public string NextUserName { get; set; }

        public string NextUserPhone { get; set; }
    }
}