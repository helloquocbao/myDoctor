using myDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myDoctor.Controllers
{
    public class AboutController : Controller
    {

        myDoctorEntities4 data = new myDoctorEntities4();
        private List<BacSi> listBacSi()
        {
            return data.BacSis.ToList();
        }
        public ActionResult About()
        {
            var listbs = listBacSi();
            return View(listbs);
        }
    }
}