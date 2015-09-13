﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShareMyThings.Controllers.ViewModelData;
using ShareMyThings.Models.Util;
using ShareMyThings.ViewModel.Use;
using ShareMyThings.Models.Use;

namespace ShareMyThings.Controllers
{
    public class UseController : Controller
    {
        // GET: Use
        public ActionResult Index()
        {
            return View();
        }


        // ReSharper disable once InconsistentNaming
        public ActionResult OK(string id)
        {
            // Use Case
            // I: User selects OK on th Start page
            // R: A welcome message is shown holding user name and item name from reservation.
            // R: The end time and the end time with slack is shown.
            // R: Next reservation start time with slack and start time is shwon if there is an reservation at the same date.
            //
            // I: The user can refresh the page to see the next reservation by pressing Refresh

            //var model = new UserControllerViewModel
            //{
            //    UserName = "Villy Ib Jørgensen"
            //};

            //long itemId;
            //if (long.TryParse(id, out itemId))
            //{
            //    model.ItemId = itemId;
            //    model.ItemName = ItemNameMap.ToName(itemId);
            //}
            //else
            //{
            //    model.ItemId = ItemNameMap.ToId(id);
            //    model.ItemName = id;
            //}

            //return View(model);

            var viewModel = new OkViewModel();

            var useLiveData = true;
            Ok modelData;

            { // DemoData
                var template = new Ok
                {
                    ItemId = 1,
                    UserName = "UserName",
                    ItemName = "ItemName",
                    ReservationEnd = DateTime.Now,
                    ReservationSlackEnd = DateTime.Now,
                    ShowNextReservation = true,
                    NextReservationSlackStart = DateTime.Now,
                    NextReservationStart = DateTime.Now,
                    NextUserName = "NextUserName",
                    NextUserPhone = "NextUserPhone",
                    Now = DateTime.Now,
                };

                string demoValue;

                if (new DemoUtil<Ok>().LoadDemo(out modelData, out demoValue, Request, id, template))
                {
                    viewModel.DemoValue = demoValue;
                    useLiveData = false;
                }
            }

            if (useLiveData)
            {
                // TODO get data from Model.
                modelData = new Ok
                {
                    ItemId = 1,
                    UserName = "UserName",
                    ItemName = "ItemName",
                    ReservationEnd = DateTime.Now,
                    ReservationSlackEnd = DateTime.Now,
                    ShowNextReservation = true,
                    NextReservationSlackStart = DateTime.Now,
                    NextReservationStart = DateTime.Now,
                    NextUserName = "NextUserName",
                    NextUserPhone = "NextUserPhone",
                    Now = DateTime.Now,
                };
            }


            viewModel.Headline = "Headline";

            var now = modelData.Now;

            {
                var current = modelData.ReservationEnd;


                string due;
                string time;

                TimeSpan difference;


                if (current.Date != now.Date)
                {
                    // not today
                    time = string.Format("{0:yyyy-MM-dd HH:mm}", current);
                }
                else
                {
                    // today
                    time = string.Format("{0:HH:mm}", current);
                }


                var isDue = now > current;
                if (isDue)
                {
                    due = "passed";
                }
                else
                {
                    difference = current.Subtract(now);

                    if (difference.TotalHours > 24.0d)
                    {
                        // more than 24 hours.
                        due = string.Format("{0}+ h", (int)difference.TotalHours);
                    }
                    else
                    {
                        due = string.Format(@"{0:hh\:mm}", difference);
                    }
                }

                viewModel.ReservationEnd = new OkViewModelRow { Alert = "", Due = due, Time = time };
            }

            {
                var current = modelData.ReservationSlackEnd;
                var isDue = now > current;

                string due;
                string time;

                TimeSpan difference;

                if (isDue)
                {
                    due = "passed";
                    difference = now.Subtract(current);
                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("-{0} days", (int)difference.TotalDays);
                    }
                    else
                    {
                        time = string.Format(@"-{0:hh\:mm}", difference);
                    }
                }
                else
                {
                    difference = current.Subtract(now);

                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("{0:yyyy-MM-dd}", current);
                        due = string.Format("{0} h", (int)difference.TotalHours);
                    }
                    else
                    {
                        time = string.Format(@"{0:HH\:mm}", current);
                        due = difference.ToString(@"hh\:mm");
                    }
                }
                viewModel.ReservationSlackEnd = new OkViewModelRow { Alert = "", Due = due, Time = time };
            }

            {
                var current = modelData.NextReservationSlackStart;
                var isDue = now > current;

                string due;
                string time;

                TimeSpan difference;

                if (isDue)
                {
                    due = "passed";
                    difference = now.Subtract(current);
                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("-{0} days", (int)difference.TotalDays);
                    }
                    else
                    {
                        time = string.Format(@"-{0:hh\:mm}", difference);
                    }
                }
                else
                {
                    difference = current.Subtract(now);

                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("{0:yyyy-MM-dd}", current);
                        due = string.Format("{0} h", (int)difference.TotalHours);
                    }
                    else
                    {
                        time = string.Format(@"{0:HH\:mm}", current);
                        due = difference.ToString(@"hh\:mm");
                    }
                }
                viewModel.NextReservationSlackStart = new OkViewModelRow { Alert = "", Due = due, Time = time };
            }

            {
                var current = modelData.NextReservationStart;
                var isDue = now > current;

                string due;
                string time;

                TimeSpan difference;

                if (isDue)
                {
                    due = "passed";
                    difference = now.Subtract(current);
                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("-{0} days", (int)difference.TotalDays);
                    }
                    else
                    {
                        time = string.Format(@"-{0:hh\:mm}", difference);
                    }
                }
                else
                {
                    difference = current.Subtract(now);

                    if (difference.TotalDays > 1.0d)
                    {
                        time = string.Format("{0:yyyy-MM-dd}", current);
                        due = string.Format("{0} h", (int)difference.TotalHours);
                    }
                    else
                    {
                        time = string.Format(@"{0:hh\:mm}", difference);
                        due = difference.ToString(@"hh\:mm");
                    }
                }
                viewModel.NextReservationStart = new OkViewModelRow { Alert = "", Due = due, Time = time };
            }

            viewModel.ShowNextReservation = modelData.ShowNextReservation;
            viewModel.NextReservationDetails = "details";

            return View(viewModel);

        }


        public ActionResult SelectItem()
        {
            var viewModel = new SelectItemViewModel
            {
                Headline = "Select an action on an item",
            };

            var useLiveData = true;
            Select modelData;

            { // DemoData
                var template = new Select
                {
                    ItemList = new List<SelectRow>
                    {
                        new SelectRow {Key=1,Display =  "ItemName" }
                    }
                };

                string demoValue;

                if (new DemoUtil<Select>().LoadDemo(out modelData, out demoValue, Request, null, template))
                {
                    viewModel.DemoValue = demoValue;
                    useLiveData = false;
                }
            }

            if (useLiveData)
            {
                // TODO get data from Model.
                modelData = new Select { ItemList = new List<SelectRow>() };
            }

            var itemList = new List<ItemRow>();
            foreach (var row in modelData.ItemList)
            {
                itemList.Add(new ItemRow { Key = row.Key, Display = row.Display, Url = string.Format("OK/{0}", row.Key) });
            }

            viewModel.ItemList = itemList;

            return View(viewModel);
        }


        public ActionResult ChangeStartValue(string id)
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


            var viewModel = new ChangeStartValueViewModel();

            var useLiveData = true;
            ChangeStartValue modelData;

            { // DemoData
                var template = new ChangeStartValue
                {
                    Id = 1,
                    Name = "Item name",
                    Value = 123456m,
                    UnitsName = "unit name",
                    ValueLabel = "odometer",
                };

                string demoValue;

                if (new DemoUtil<ChangeStartValue>().LoadDemo(out modelData, out demoValue, Request, id, template))
                {
                    viewModel.DemoValue = demoValue;
                    useLiveData = false;
                }
            }

            if (useLiveData)
            {
                // TODO get data from Model.
                modelData = new ChangeStartValue
                {
                    Id = 1,
                    Name = "Item name",
                    Value = 123456m,
                    UnitsName = "unit name",
                    ValueLabel = "odometer",
                };
            }


            viewModel.Headline = string.Format("Please enter new {0} value for {1}"
                , modelData.ValueLabel
                , modelData.Name
                );
            viewModel.ItemValueCurrent = modelData.Value;
            viewModel.ItemValueUnitName = modelData.UnitsName;
            viewModel.ItemValueNew = modelData.Value;
            viewModel.Id = modelData.Id.ToString();
            viewModel.ItemName = modelData.Name;

            return View(viewModel);

        }

        /// <summary>
        /// Start a trip.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Start(string id)
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
            //
            // E5: The id is null or illegal (user pressed start on the menu)
            // R: The SelectItem page is shown

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


        public ActionResult Stop(string id)
        {
            return View();
        }


        public ActionResult Change(string id)
        {
            return View();
        }


    }
}
