using myDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myDoctor.Controllers
{
    public class userController : Controller
    {
       

        // GET: user
        DoctorQBEntities data = new DoctorQBEntities();
        public ActionResult logout()
        {
            Session["taikhoan"] = null;
            return Redirect("/");

        }

        public ActionResult account()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var account = from p in data.KhachHangs where p.idkh ==accountcheck.idkh  select p;
            return View(account.Single());
        }

        public ActionResult lichkham()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            return View(data.DatLiches.OrderByDescending(a => a.ngaydat).Where(a=>a.idkh==accountcheck.idkh).ToList());
            ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lichkham(FormCollection collection, DatLich dl)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var idbs = 2;
            var idkh = accountcheck.idkh;
            string ngay = collection["datetime"];

            DateTime datee = DateTime.Parse(ngay);
            var mota = collection["mota"];

            dl.idbs = idbs;
            dl.idkh = idkh;
            dl.ngaydat = datee;
            dl.trieuchung = mota;
            data.DatLiches.Add(dl);
            data.SaveChanges();
            return RedirectToAction("index", "home");
        }
    }
}