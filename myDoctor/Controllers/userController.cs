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
        myDoctorEntities data = new myDoctorEntities();
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
            var account = from p in data.KhachHangs where p.idKhachHang == accountcheck.idKhachHang select p;
            return View(account.Single());
        }

        public ActionResult lichkham()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a=>a.idKhachHang == accountcheck.idKhachHang).ToList());
            ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lichkham(FormCollection collection, LichKham dl)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var idbs = 2;
            var idkh = accountcheck.idKhachHang;
            string ngay = collection["datetime"];

            DateTime datee = DateTime.Parse(ngay);
            var mota = collection["mota"];

            dl.idBacSi = idbs;
            dl.idKhachHang = idkh;
            dl.ngaydat = datee;
            dl.trieuchung = mota;
            data.LichKhams.Add(dl);
            data.SaveChanges();
            return RedirectToAction("index", "home");
        }
    }
}