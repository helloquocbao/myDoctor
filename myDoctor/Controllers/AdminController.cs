using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myDoctor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DoctorController data = new DoctorController();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult bacsi()
        {

            return View();
        }

        public ActionResult login()
        {

            return View();
        }
    }
}