using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using myDoctor.Models;

namespace myDoctor.Controllers
{
    public class HomeController : Controller
    {
        myDoctorEntities4 data = new myDoctorEntities4();
        

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection, LichKham dl, HttpPostedFileBase fileUpload)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                try
                {
                    KhachHang accountcheck = Session["taikhoan"] as KhachHang;
                    var idkh = accountcheck.idKhachHang;
                    int idbs = int.Parse(collection["bacsi"]);
                    int atuoi = int.Parse(collection["tuoi"]);
                    string ngay = collection["datetime"];
                    string hoten = collection["hoten"];
                    var mota = collection["mota"];
                    string anh;
                    if(fileUpload == null) {
                         anh = null;
                    }
                    else
                    {
                        string strDateTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
                        string path = Path.Combine(Server.MapPath("~/HinhAnh/"), strDateTime + Path.GetFileName(fileUpload.FileName));
                         anh = strDateTime + Path.GetFileName(fileUpload.FileName);
                        fileUpload.SaveAs(path);
                    }

                    if (ngay == null || hoten.Length == 0 || atuoi == null || mota.Length == 0)
                    {
                        ViewData["Loi3"] = "Vui lòng điền đầy đủ thông tin";
                        return View();
                    }
                    else
                    {
                        DateTime datee = DateTime.Parse(ngay);

                        if (atuoi <= 3)
                        {
                            ViewData["Loi11"] = "Xin lỗi phòng khám chỉ khám trẻ trên 3 tuổi";
                            return View();
                        }
                        if (datee <= DateTime.Now.AddHours(2))
                        {
                            ViewData["Loi1"] = "Vui lòng chọn thời gian lớn hơn thời gian hiện tại 2 giờ";
                            return View();
                        }
                        
                        
                        dl.idBacSi = idbs;
                        dl.hoten = hoten;
                        dl.idKhachHang = idkh;
                        dl.ngaydat = datee;
                        dl.trieuchung = mota;
                        dl.anhchitiet = anh;
                        data.LichKhams.Add(dl);
                        dl.tinhTrang = false;

                       
                        data.SaveChanges();


                        ViewData["Loi2"] = "Đặt lịch thành công, bạn có thể kiểm tra trong thông tin cá nhân";
                        return Redirect("~/user/lichkham");
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Loi3"] = "Vui lòng điền đầy đủ thông tin";
                }
            }
            return View();
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
                ViewData["Loi1"] = "Vui lòng nhập SDT để đăng nhập";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.sdt == sdt && n.passkh == pass);
                if (kh != null)
                {


                    Session["taikhoan"] = kh;
                    return Redirect("/");

                }

                else
                    ViewData["Loi3"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }


        [HttpGet]
        public ActionResult signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult signin(FormCollection collection,  KhachHang kh, ThanNhan tn)
        {
            var hoten = collection["họ&tên"];
            var email = collection["email"];
            var pass = collection["password"];
            var repass = collection["repassword"];
            var phone = collection["SDT"];
                      
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }         
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(repass))
            {
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(phone))
            {
                ViewData["Loi5"] = "Vui lòng nhập sdt";
            }
            else if (pass != repass)
            {
                ViewData["Loi8"] = "Mật khẩu không giống nhau";
            }         
            else
            {
                try
                {
                    KhachHang checkkh = data.KhachHangs.SingleOrDefault(n => n.sdt == phone);
                    if (checkkh != null)
                    {
                        ViewData["Loi0"] = "SĐT đã tồn tại, vui lòng chọn SĐT khác";
                        return this.signin();
                    }

                    if(phone.Length != 10)
                    {
                        {
                            ViewData["Loi7"] = "Vui lòng nhập đúng số điện thoại";
                            return this.signin();
                        }
                    }
                    kh.tenkh = hoten;
                    kh.email = email;
                    kh.passkh = pass;
                    kh.sdt = phone;
                    tn.sdtThanNhan = phone;
                    tn.tenThanNhan = hoten;
                    
                    data.KhachHangs.Add(kh);
                    data.ThanNhans.Add(tn);

                    
                    data.SaveChanges();
                    return RedirectToAction("login");
                }
                catch (Exception ex)
                {
                    ViewData["Loi10"] = "Vui lòng nhập đủ thông tin";
                }
            }
            return this.signin();
        }

      
       
    }
}