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

    public class PhoneNumber 
    {
        public  int CountryCode { get; set; }
        public int NetworkPrefix { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// Render Phone number for human reading 'p' [Network prefix ] Number, 'P' Country Code [NetworkPrefix] Number.
        /// - p: for human reading: [Network prefix ] Number,
        /// - P: for human reading: Country Code [NetworkPrefix] Number.
        /// - s/S for SMS
        /// - t/T for Telephon call
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            var rendering = "{2:#### ####}"; // Number

            if (NetworkPrefix > 0)
            {
                rendering = "{1} " + rendering; // NetworkPrefix
            }
             
            if ("P".Equals(format, StringComparison.Ordinal))
            {
                rendering = "+{0} " + rendering; // Country Code
            }

            if ("s".Equals(format, StringComparison.OrdinalIgnoreCase))
            {
                rendering = "sms:+{0}{1}{2}";
            }

            if ("t".Equals(format, StringComparison.OrdinalIgnoreCase))
            {
                rendering = "tel:+{0}{1}{2}";
            }

            return string.Format(rendering
                , CountryCode==0 ? 45 : CountryCode
                , NetworkPrefix
                , Number
            );
        }

        public override string ToString()
        {
            return ToString("P");
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

        public PhoneNumber NextUserPhone { get; set; }
    }
}