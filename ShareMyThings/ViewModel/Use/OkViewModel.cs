using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

using ShareMyThings.Models.Util;

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

    public class PhoneNumber : ObjectWrapper<string>
    {
        public override string ToString()
        {
            long t1;
            var t2 = Value.Replace(" ","");
            return long.TryParse(t2, out t1) ? 
                t1 > 999999999 
                    ? String.Format("+{0: ## #### ####}", t1) 
                    : string.Format("0:#### ####", t1) 
                : Value;
        }
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

        public long NextUserPhoneForUrl { get; set; }

        public PhoneNumber NextUserPhoneForDisplay { get; set; }
    }
}