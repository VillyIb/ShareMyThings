using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShareMyThings.Controllers.ViewModelData
{
    public class UserControllerViewModel : BaseViewModel
    {
        /// <summary>
        /// The primary key of the shared item.
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// The display name of the shared item.
        /// </summary>
        public String ItemName { get; set; }

        /// <summary>
        /// The primarry key of the user currently holding a reservation to the item.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// The username of the user currently holding a reservation to the item.
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Value checked opon Start and updated upon Stop.
        /// </summary>
        public float ItemUsageValue { get; set; }

        /// <summary>
        /// The end time for the reservation.
        /// </summary>
        public String CurrentReservationEndTime { get; set; }

        /// <summary>
        /// The end time for the reservation incl. slack.
        /// </summary>
        public String CurrentReservationSlackEndTime { get; set; }

        /// <summary>
        /// True: There is at least one reservation later todday.
        /// False: There are no other reservation later today.
        /// </summary>
        public bool ShowNextReservation { get; set; }

        /// <summary>
        /// The start time for the next reservation incl. slack.
        /// </summary>
        public String NextReservationSlackStartTime { get; set; }

        /// <summary>
        /// The start time for the next reservation.
        /// </summary>
        public String NextReservationStartTime { get; set; }

        public bool ShowEarlyReturnMessage { get; set; }

        public String NetxUserName { get; set; }

        public String NextUserPhone { get; set; }
    }


}