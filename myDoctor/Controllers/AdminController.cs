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
        myDoctorEntities3 data = new myDoctorEntities3();
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

        public ActionResult taikhoanDK()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View(data.KhachHangs.ToList());
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
        [HttpGet]
        public ActionResult account()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi accountcheck = Session["tkBacSi"] as BacSi;           
            var account = from p in data.BacSis where p.idBacSi == accountcheck.idBacSi select p;
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult account(FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi bs = Session["tkBacSi"] as BacSi;
            var hoten = collection["hoten"];
            var email = collection["email"];
            var pass = collection["password"];
            var mota = collection["mota"];
            string anh;
            if (fileUpload == null)
            {
                anh = null;
                ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
                return RedirectToAction("account", "admin");
            }
            else
            {
                string strDateTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
                string path = Path.Combine(Server.MapPath("~/hinhbacsi/"), strDateTime + Path.GetFileName(fileUpload.FileName));
                anh = strDateTime + Path.GetFileName(fileUpload.FileName);
                fileUpload.SaveAs(path);
            }
            if (hoten == null || email == null || mota == null || pass== null)
            {
                ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
                return RedirectToAction("account", "admin");
            }
            else
            {
                if (pass.Equals(bs.passbs))
                {
                    var acc = data.BacSis.Where(s => s.idBacSi == bs.idBacSi).First<BacSi>();
                    acc.email = email;
                    acc.tenbs = hoten;
                    acc.urlanh = anh;
                    acc.motabs = mota;
                    acc.idHocVi = 1;
                    data.SaveChanges();
                   ;
                }
                else
                {
                    ViewData["Loi1"] = "Sai mật khẩu!, vui lòng thử lại";
                    return RedirectToAction("account", "admin");
                }                            
            }

            return RedirectToAction("index", "admin");
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

        public ActionResult hosoKhachHang(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

           

            return View();
        }

        [HttpGet]
        public ActionResult ghiketqua(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            var kh = from s in data.LichKhams where s.idDatLich == id select s;
            return View(kh.Single());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ghiketqua(FormCollection collection, KetQuaKham kq)
        {
            try
            {
                int idLichKham = int.Parse(collection["idlichkham"]);
                var ketqua = collection["ketqua"];
                var hdThuoc = collection["hdthuoc"];
                var phiKham = int.Parse(collection["chiphi"]);
                if(ketqua == null || hdThuoc == null || idLichKham == null || phiKham == null)
                {
                    ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
                }
                else
                {
                    kq.idDatLich = idLichKham;
                    kq.ketqua = ketqua;
                    kq.hdthuoc = hdThuoc;
                    kq.tienkham = phiKham;
                    data.KetQuaKhams.Add(kq);
                    data.SaveChanges();
                    var lichChange = data.LichKhams.Where(s => s.idDatLich == idLichKham).First<LichKham>();
                    lichChange.tinhTrang = true;
                    data.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
            }

            return RedirectToAction("lichkham", "admin");
        }
    }
}