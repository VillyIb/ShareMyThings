using System;
using System.Collections.Generic;

namespace ShareMyThings.Models.Use
{
    // --- SelectItem ---

    public class SelectRow
    {
        public int Key { get; set; }

        public String Display { get; set; }
    }

    public class Select
    {
        public List<SelectRow> ItemList { get; set; } = new List<SelectRow>();
    }
       
}