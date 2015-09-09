using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareMyThings.Controllers
{
    public class AccountingController : Controller
    {
        // GET: Accounting
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Payable()
        {
            return View();
        }


        public ActionResult Receivable()
        {
            return View();
        }


    }
}