using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.ViewModel.Use
{
    public class ChangeStartValueViewModel : BaseViewModel
    {
        public string ItemName { get; set; }

        public decimal ItemValueCurrent { get; set; }

        public decimal ItemValueNew { get; set; }

        public string ItemValueUnitName { get; set; }

        public string Id { get; set; }
    }
}