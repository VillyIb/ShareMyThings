using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareMyThings.Models.Util
{
    public class ItemNameMap
    {
        public static String ToName(long itemId)
        {
            var result = String.Empty;

            switch (itemId)
            {
                case 0:
                    {
                        result = "Select and item!";
                    }
                    break;
                case 1:
                    {
                        result = "Aspargsen";
                    }
                    break;
                case 2:
                    {
                        result = "Bønnen";
                    }
                    break;
                case 3:
                    {
                        result = "Chilien";
                    }
                    break;
                case 4:
                    {
                        result = "Rosinen";
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        public static long ToId(String name)
        {
            var result = 0L;

            do
            {
                if(String.IsNullOrWhiteSpace(name))
                {
                    result = 0;
                    break;
                }

                if ("Aspargsen".IndexOf(name, StringComparison.OrdinalIgnoreCase)==0)
                {
                    result = 1;
                    break;
                }

                if ("Bønnen".IndexOf(name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    result = 2;
                    break;
                }

                if ("Chilien".IndexOf(name, StringComparison.OrdinalIgnoreCase)==0)
                {
                    result = 3;
                    break;
                }

                if ("Rosinen".IndexOf(name, StringComparison.OrdinalIgnoreCase)==0)
                {
                    result = 4;
                    break;
                }


            } while (false);

            return result;
        }
    }
}