using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.ViewModel.Use
{
    public class ItemRow
    {
        public int Key { get; set; }
        public String Display { get; set; }
        public String Url { get; set; }
    }

    public class SelectItemViewModel
    {
        public String Headline { get; set; }
        public List<ItemRow>  ItemList { get; set; }

        public String DemoValue { get; set; }
    }
}