using myDoctor.Models;
using myDoctor.Models.ViewModel;
using System;

using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace myDoctor.Controllers
{
    public class userController : Controller
    {


        // GET: user
        myDoctorEntities3 data = new myDoctorEntities3();
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
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == accountcheck.idKhachHang).ToList());
            ;
        }

        public ActionResult XemKetQua(int id)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            var chitet = from s in data.KetQuaKhams 
                         join c in data.LichKhams on s.idDatLich equals c.idDatLich
                         join a in data.BacSis on c.idBacSi equals a.idBacSi
                         where s.idDatLich == id && c.idDatLich == s.idDatLich && a.idBacSi == c.idBacSi
                         select new KetQuaKhamViewModel
                         {
                             idDatLich = c.idDatLich,
                             idKetQua = s.idKetQua,
                             idBacSi = a.idBacSi,
                             tenbs = a.tenbs,
                             ketqua = s.ketqua,
                             hdthuoc = s.hdthuoc,
                             tienkham = s.tienkham,
                             ChuyenKhoa = a.HocVi.ChuyenKhoa,
                         };

            return PartialView(chitet.Single());
        }

        public ActionResult ThanNhan()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var lsThanNhan = from s in data.chitietThanNhans where s.idkhachHang == accountcheck.idKhachHang || s.sdtThanNhan == accountcheck.sdt select s;

            return View(lsThanNhan);
        }

        public ActionResult addthannhan()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addthannhan(FormCollection collection)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                KhachHang accountcheck = Session["taikhoan"] as KhachHang;
                var idkh = accountcheck.idKhachHang;
                var sdt = collection["sdt"];
                var mqh = collection["mqh"];
                if (mqh.Length < 2 || mqh.Length > 30)
                {
                    ViewData["Loi1"] = "Mối quán hệ với người thân phải từ 2 đến 30 kí tự";
                    return this.View();
                }

                int check = data.chitietThanNhans.Where(n => n.idctThanNhan == idkh && n.sdtThanNhan == sdt).Count();
                if (check == 0)
                {
                    try { 
                    var ctthannhan = new chitietThanNhan()
                    {

                        idkhachHang = idkh,
                        sdtThanNhan = sdt,
                        qHeVoiBenhNhan = mqh,
                    };
                    data.chitietThanNhans.Add(ctthannhan);
                    data.SaveChanges();
                    return RedirectToAction("thannhan", "user");
                    }
                    catch(Exception ex)
                    {
                        return RedirectToAction("thannhan", "user");
                    }
                }
                else
                {
                    ViewData["Loi2"] = "Bạn đã thêm mối quan hệ từ trước!";
                    return this.View();
                }
                
            }

            return PartialView();
        }

        public ActionResult xoaThanNhan(int id)
        {

            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }             
            var thanNhan = data.chitietThanNhans.Where(s => s.idctThanNhan ==id).First<chitietThanNhan>();
            data.chitietThanNhans.Remove(thanNhan);
            data.SaveChanges();
            return RedirectToAction("thannhan", "user");
        }

        public ActionResult nguoibenh()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var lsThanNhan = from s in data.chitietThanNhans where s.sdtThanNhan == accountcheck.sdt select s;        
            return View(lsThanNhan);
        }

        public ActionResult thongtinbenhnhan(int id)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == id).ToList());
        }

        public ActionResult xemAnhThongTinBenh(int id)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            var lichkham = from s in data.LichKhams where s.idDatLich == id select s;
            return View(lichkham.Single());
        }


        public ActionResult video_call()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            return View();
        }

        public ActionResult doiMatKhau()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult doiMatKhau(FormCollection collection)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accCheck = Session["taikhoan"] as KhachHang;
     
            var oldpass = collection["oldpass"];
            var newpass = collection["newpass"];
            var repass = collection["repass"];

            if(newpass.Length < 8 || newpass.Length > 35)
            {
                ViewData["Loi2"] = "Mật khẩu mới phải lớn hơn 8 và bé hơn 35 kí tự";
                return this.View();
            }

            if (oldpass.Equals(accCheck.passkh))
            {
              if(newpass.Equals(repass))
                {


                    var accChange = data.KhachHangs.Where(s=>s.idKhachHang==accCheck.idKhachHang).First<KhachHang>();
                    accChange.passkh = newpass;
                    data.SaveChanges();
                    return RedirectToAction("login", "home");
                }
                else
                {
                    ViewData["Loi1"] = "Mật khẩu xác nhận không chính xác";
                   
                }               
            }
            else
            {
                ViewData["Loi0"] = "Mật khẩu không chính xác";
            }
                                               
            return this.View();
        }
    }
}
