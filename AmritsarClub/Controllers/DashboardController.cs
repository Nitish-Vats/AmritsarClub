using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmritsarClub.Models;

namespace AmritsarClub.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult SaleOrderDashboard()
        {
            return View();
        }

        public ActionResult PurchaseDashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult dashboard()
        {
            return View();
        }


    }
}