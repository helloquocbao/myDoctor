using myDoctor.Models;
using myDoctor.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myDoctor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        myDoctorEntities1 data = new myDoctorEntities1();
        public ActionResult Index()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View();
        }

        public ActionResult bacsi()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View(data.BacSis.ToList());
        }

        public ActionResult login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(FormCollection collection)
        {
            var sdt = collection["sdt"];
            var pass = collection["password"];
            if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Loi1"] = "Vui lòng nhập sdt để đăng nhập";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                BacSi bs = data.BacSis.SingleOrDefault(n => n.sdt == sdt && n.passbs == pass);
                if (bs != null)
                {

                    Session["tkBacSi"] = bs;
                    return RedirectToAction("index", "admin");

                }

                else
                    ViewData["Loi3"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult logout()
        {
            Session["tkBacSi"] = null;
            return RedirectToAction("login", "admin");

        }

        public ActionResult account()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi accountcheck = Session["tkBacSi"] as BacSi;
            var account = from p in data.BacSis where p.idBacSi == accountcheck.idBacSi select p;
            return View(account.Single());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult account(HttpPostedFileBase fileupload)
        { 
            BacSi bs = Session["tkBacSi"] as BacSi;
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/hinhbacsi"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";                           
                    }
                    else
                    {
                        fileupload.SaveAs(path);

                    }
                    bs.urlanh = fileName;
                    UpdateModel(bs);
                    data.SaveChanges();
                }
                return RedirectToAction("account");
            }
         
        }

        public ActionResult lichKham()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi accountcheck = Session["tkBacSi"] as BacSi;

        
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idBacSi == accountcheck.idBacSi && a.ngaydat > DateTime.Now).ToList());
        }


        public ActionResult answerVideoCall()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View();
         }

        public ActionResult xemKetQua(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var ketQua = from a in data.KetQuaKhams
                         join b in data.LichKhams on a.idDatLich equals b.idDatLich
                         join c in data.BacSis on b.idBacSi equals c.idBacSi                      
                         where a.idDatLich == id && a.idDatLich == b.idDatLich && c.idBacSi == b.idBacSi
                         select new KetQuaKhamViewModel
                         {
                             idDatLich = a.idDatLich,
                             idKetQua = a.idKetQua,
                             idBacSi = b.idBacSi,
                             tenbs = c.tenbs,
                             ketqua = a.ketqua,
                             hdthuoc = a.hdthuoc,
                             tienkham = a.tienkham,
                             ChuyenKhoa = c.HocVi.ChuyenKhoa, 
                         };
            
            return View(ketQua);
        }


    }
}