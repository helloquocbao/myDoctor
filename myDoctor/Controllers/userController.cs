using myDoctor.Models;
using myDoctor.Models.ViewModel;
using System;

using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace myDoctor.Controllers
{
    public class userController : Controller
    {


        // GET: user
        myDoctorEntities4 data = new myDoctorEntities4();
        public ActionResult logout()
        {
            Session["taikhoan"] = null;
            return Redirect("/");

        }

        [HttpGet]
        public ActionResult account()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult account(FormCollection collection)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var hoten = collection["hoten"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var pass = collection["pass"];
            if (hoten == "" || diachi == "" || pass == "")
            {
                ViewData["Loi0"] = "Vui lòng điền đủ thông tin";
                return this.View();
            }
            if(hoten != "" && diachi != "" && pass != "")
            {
                if (pass.Equals(accountcheck.passkh))
                {
                    var accChange = data.KhachHangs.Where(s => s.idKhachHang == accountcheck.idKhachHang).First<KhachHang>();
                    accChange.diachi = diachi;
                    accChange.tenkh = hoten;
                    accChange.email = email;
                    data.SaveChanges();
                    return RedirectToAction("account", "user");
                }
                else
                {
                    ViewData["Loi1"] = "Mật khẩu không chính xác";
                    return this.View();
                }


            }

            return View();
        }

        public ActionResult lichkham()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == accountcheck.idKhachHang && a.tinhTrang ==false && a.ngaydat >=DateTime.Now).ToList());
            ;
        }

        public ActionResult lichkhamroi()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == accountcheck.idKhachHang && a.tinhTrang==true).ToList());
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

        [HttpGet]
        public ActionResult hosoKhachHang()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            int check = data.benhAns.Where(n => n.idKhachHang == accountcheck.idKhachHang).Count();
            if ( check == 0)
            {
                benhAn bn = new benhAn();
                bn.idKhachHang = accountcheck.idKhachHang;
                bn.mota = "Vui lòng điền vào đây";
                data.benhAns.Add(bn);
                data.SaveChanges();
                return RedirectToAction("hosoKhachHang", "user");
            }
            else
            {
                return View(data.benhAns.Where(s=>s.idKhachHang== accountcheck.idKhachHang).Single());
            }
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult hosoKhachHang(FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang kh = Session["taikhoan"] as KhachHang;                     
            var mota = collection["mota"];
            string anh;
            if (fileUpload == null)
            {
                anh = null;
                ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
                return RedirectToAction("hosoKhachHang", "user");
            }
            else
            {
                string strDateTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
                string path = Path.Combine(Server.MapPath("~/HinhBenhAn/"), strDateTime + Path.GetFileName(fileUpload.FileName));
                anh = strDateTime + Path.GetFileName(fileUpload.FileName);
                fileUpload.SaveAs(path);
            }
            
               
                 var hoso = data.benhAns.Where(s => s.idKhachHang == kh.idKhachHang).First<benhAn>();
                 hoso.urlanh = anh;
                 hoso.mota = mota;
               
                 data.SaveChanges();


            return RedirectToAction("hosoKhachHang", "user");
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

        public ActionResult donthuoc(int id)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            var danhsach = from a in data.ChiTietThuocs where a.idKetQua == id select a;


            return View(data.ChiTietThuocs.Where(a => a.idKetQua == id).ToList());

        }
    }
}
