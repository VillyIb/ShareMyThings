using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShareMyThings.Controllers.ViewModelData;
using ShareMyThings.Models.Util;

namespace ShareMyThings.Controllers
{
    public class UseController : Controller
    {
        // GET: Use
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OK(String id)
        {
            // Use Case
            // I: User selects OK on th Start page
            // R: A welcome message is shown holding user name and item name from reservation.
            // R: The end time and the end time with slack is shown.
            // R: Next reservation start time with slack and start time is shwon if there is an reservation at the same date.
            //
            // I: The user can refresh the page to see the next reservation by pressing Refresh

            var model = new UserControllerViewModel
            {
                UserName = "Villy Ib Jørgensen"
            };

            long itemId;
            if (long.TryParse(id, out itemId))
            {
                model.ItemId = itemId;
                model.ItemName = ItemNameMap.ToName(itemId);
            } 
            else
            {
                model.ItemId = ItemNameMap.ToId(id);
                model.ItemName = id;
            }

            return View(model);
        }


        public ActionResult ChangeStartValue(String id)
        {
            // Use Case
            // I: User selects Change from the Start page.
            // R: this page is shwon with the last stop value.
            // I: User enters new value and press Save
            // R: The Start page is shown
            //
            // Exceptions-
            // E1 the value the user entered is out of range or not nummeric.
            // - a value can not differ the last value by more than 9999 and must be greater than 0.
            // rules may be refined
            // R: An error message is shown
            // 
            // I: Second press Save on the same value 
            // R: The value is accepted the value and the Start page is shown

            ViewBag.Item = id ?? "";
            ViewBag.OdometerValue = "123.456";
            return View();
        }

        /// <summary>
        /// Start a trip.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Start(String id)
        {
            // Use Case
            // I: User scans barcode  
            // R: this page is shown with with the last stop value.
            // I: User accepts the shown value by pressing OK
            // R: The OK page is shown
            //
            // Exceptions-
            // E1: The last trip was not stopped
            // R: The Stop page is shown with an error message
            //
            // E2: The user does not accept the shown value
            // R: The ChangeStartValue page is shown
            // 
            // E3: The user press the 'Not me' button
            // R: the booking update page is shown.
            //
            // E4: There are no reservation at the curren time
            // R: the booking page is shown.

            var model = new UserControllerViewModel
            {
                UserName = "Villy Ib Jørgensen"
            };

            long itemId;
            if (long.TryParse(id, out itemId))
            {
                model.ItemId = itemId;
                model.ItemName = ItemNameMap.ToName(itemId);
            }
            else
            {
                model.ItemId = ItemNameMap.ToId(id);
                model.ItemName = ItemNameMap.ToName(model.ItemId);
            }

            return View(model);
        }


        public ActionResult Stop(String id)
        {
            return View();
        }


        public ActionResult Change(String id)
        {
            return View();
        }


    }
