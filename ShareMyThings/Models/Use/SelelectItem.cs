using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.Models.Use
{
    public class SelectItemRow
    {
        public int Key { get; set; }
        public String Display { get; set; }
    }

    public class SelectItem
    {
        public List<SelectItemRow> ItemList { get; set; } = new List<SelectItemRow>();
    }
}