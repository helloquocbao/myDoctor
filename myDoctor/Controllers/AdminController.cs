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
        myDoctorEntities4 data = new myDoctorEntities4();

        public ActionResult Index()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            int countKH = (from a in data.KhachHangs select a).Count();
            int countLK = (from a in data.LichKhams where a.ngaydat == DateTime.Today select a).Count();
            int countDaKham = (from a in data.LichKhams where a.ngaydat == DateTime.Today && a.tinhTrang == true select a).Count();
            int countChKham = (from a in data.LichKhams where a.ngaydat == DateTime.Today && a.tinhTrang == false select a).Count();
            int countAdmin = (from a in data.BacSis select a).Count();
            BacSi accountcheck = Session["tkBacSi"] as BacSi;
            ViewBag.khachang = countKH;
            ViewBag.lichkham = countLK;
            ViewBag.dakham = countDaKham;
            ViewBag.chkham = countChKham;
            ViewBag.nhanvien = countAdmin;
            if(accountcheck.quyen == false)
            {
                return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.ngaydat > DateTime.Now).ToList());
            }

            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idBacSi == accountcheck.idBacSi && a.ngaydat > DateTime.Now ).ToList());
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
            if (hoten == null || email == null || mota == null || pass == null)
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
            if (accountcheck.quyen == false)
            {
                return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.ngaydat > DateTime.Now && a.tinhTrang == false).ToList());
            }

            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idBacSi == accountcheck.idBacSi && a.ngaydat > DateTime.Now && a.tinhTrang == false).ToList());
        }

        public ActionResult lichdakham()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi accountcheck = Session["tkBacSi"] as BacSi;
            if(accountcheck.quyen == null || accountcheck.quyen==false)
            {
                return View(data.LichKhams.Where(a => a.tinhTrang == true).ToList());
            }

            return View(data.LichKhams.OrderByDescending(a => a.ngaydat)    .Where(a => a.tinhTrang == true && a.idBacSi == accountcheck.idBacSi).ToList());
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
        public ActionResult chitietbs(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            var account = from p in data.BacSis where p.idBacSi == id select p;
            return View(account);
        }

        [HttpGet]
        public ActionResult thembacsi()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            
             ViewBag.Hocvi= new SelectList(data.HocVis.ToList().OrderBy(n => n.idHocVi), "idHocVi", "HocVi1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult thembacsi(FormCollection collection, BacSi bs)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            var sdt = collection["sdt"];
            var hoten = collection["ten"];
            var chucvu = collection["chucvu"];
            var hocvi = collection["hocvi"];

            if (sdt == null || hoten == null || chucvu == null)
            {
                ViewData["Loi1"] = "Vui lòng nhập đủ thông tin";
                return this.View();
            }
            else
            {
                if (sdt.Length < 10)
                {
                    ViewData["Loi2"] = "Vui lòng nhập đúng số điện thoại";
                    return this.View();
                }
               
                    try
                {
                        bool quyen = Boolean.Parse(chucvu);
                        int hocvii = int.Parse(hocvi);
                        bs.sdt = sdt;
                        bs.tenbs = hoten;
                        bs.quyen = quyen;
                        bs.idHocVi = hocvii;
                        bs.passbs = "Bs@12345";
                        BacSi accountcheck = Session["tkBacSi"] as BacSi;
                        if (accountcheck.quyen == null)
                        {
                            data.BacSis.Add(bs);
                            data.SaveChanges();
                        }
                        return RedirectToAction("bacsi", "admin");
                    }
                    catch
                    {
                        ViewData["Loi1"] = "Vui lòng thử lại";
                        return this.View();
                    }
                
            }

            return View();
        }

        public ActionResult xemthongtin(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var thongtin = from a in data.KhachHangs where a.idKhachHang == id select a;
        
           // var test ;
            var anh = from a in data.benhAns where a.idKhachHang == id select a.urlanh;
          
            try
            {
                var test = data.benhAns.Where(s => s.idKhachHang == id).First<benhAn>();
                string url = test.urlanh;
                ViewBag.anh = url;
            }
            catch { }

           
            return View(thongtin.Single());
        }

        public ActionResult xoabacsi(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi bacsi = data.BacSis.SingleOrDefault(n => n.idBacSi == id);
            BacSi accountcheck = Session["tkBacSi"] as BacSi;
            if (accountcheck.quyen == null)
            {
                if (bacsi.idBacSi == accountcheck.idBacSi)
                {
                    ViewData["Loi0"] = "Không thể xóa tài khoản đang đăng nhập";
                    return RedirectToAction("bacsi", "admin");
                }
                data.BacSis.Remove(bacsi);
                data.SaveChanges();
            }
            return RedirectToAction("bacsi", "admin");
        }

        public ActionResult Resetmatkhau(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi bacsi = data.BacSis.SingleOrDefault(n => n.idBacSi == id);
            BacSi accountcheck = Session["tkBacSi"] as BacSi;
           if(accountcheck.quyen == null) {
                if (bacsi.idBacSi == accountcheck.idBacSi)
                {
                    ViewData["Loi0"] = "Không thể Reset mật khẩu tài khoản đang đăng nhập";
                    return RedirectToAction("bacsi", "admin");
                }
            }
            var accChange = data.BacSis.Where(s => s.idBacSi ==id).First<BacSi>();
            accChange.passbs = "bs123456";
            data.SaveChanges();
            return RedirectToAction("bacsi", "admin");
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
                if (ketqua == null || hdThuoc == null || idLichKham == null || phiKham == null)
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
            catch (Exception ex)
            {
                ViewData["Loi0"] = "Vui lòng điền đầy đủ thông tin";
            }

            return RedirectToAction("lichkham", "admin");
        }

        public ActionResult xemAnhThongTinBenh(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var lichkham = from s in data.LichKhams where s.idDatLich == id select s;
            return View(lichkham.Single());
        }


        public ActionResult doiMatKhau()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult doiMatKhau(FormCollection collection)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            BacSi accCheck = Session["tkBacSi"] as BacSi;

            var oldpass = collection["oldpass"];
            var newpass = collection["newpass"];
            var repass = collection["repass"];

            if (newpass.Length < 8 || newpass.Length > 35)
            {
                ViewData["Loi2"] = "Mật khẩu mới phải lớn hơn 8 và bé hơn 35 kí tự";
                return this.View();
            }

            if (oldpass.Equals(accCheck.passbs))
            {
                if (newpass.Equals(repass))
                {


                    var accChange = data.BacSis.Where(s => s.idBacSi == accCheck.idBacSi).First<BacSi>();
                    accChange.passbs = newpass;
                    data.SaveChanges();
                    return RedirectToAction("login", "admin");
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


        public ActionResult thuoc()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            return View(data.Thuocs.ToList());
        }

        [HttpGet]
        public ActionResult themmotthuoc()
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themmotthuoc(FormCollection collection, Thuoc thuoc)
        {
            var name = collection["name"];
            var mota = collection["mota"];
            if(name == "" || mota == "")
            {
                ViewData["Loi1"] = "Vui lòng điền đầy đủ thông tin";
                return this.View();
            }
            else
            {
                thuoc.tenthuoc = name;
                thuoc.mota = mota;
                data.Thuocs.Add(thuoc);
                data.SaveChanges();
                return RedirectToAction("thuoc", "admin");
            }

            return View();
        }

        public ActionResult xoathuoc(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            Thuoc thuoc = data.Thuocs.SingleOrDefault(n => n.idthuoc == id);
            try { 
            data.Thuocs.Remove(thuoc);
            data.SaveChanges();
            }catch(Exception ex)
            {
                ViewData["Loi1"] = "Xóa lỗi, vui lòng thử lại";
            }
            return RedirectToAction("thuoc", "admin");
        }

        [HttpGet]
        public ActionResult suathongtinthuoc(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var thuoc = from a in data.Thuocs where a.idthuoc == id select a;
            return View(thuoc);              
        }

        public ActionResult suathongtinthuoc(FormCollection collection)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }
            var name = collection["name"];
            var mota = collection["mota"];
            int id =int.Parse(collection["id"]);
            if (name == "" || mota=="")
            {
                ViewData["Loi1"] = "vui lòng điền đủ thông tin";
                return RedirectToAction("thuoc", "admin");
            }
            else
            {
                var thuoc = data.Thuocs.Where(s => s.idthuoc == id).First<Thuoc>();
                thuoc.tenthuoc = name;
                thuoc.mota = mota;
                data.SaveChanges();
                return RedirectToAction("thuoc", "admin");
            }
            return RedirectToAction("thuoc", "admin");
        }
        
        public ActionResult check(int id)
        {
            int checka = data.ChiTietThuocs.Where(a => a.idKetQua == id).Count();
            if (checka == 0)
            {
                return Redirect("/admin/themthuoc/" + id);
            }
            else
            {
                return Redirect("/admin/dachsachthuoc/" + id);
               
            }
        }

        [HttpGet]
        public ActionResult themThuoc(int id)
        {
            ViewBag.idketqua = id;
                ViewBag.thuoc = new SelectList(data.Thuocs.ToList().OrderBy(n => n.idthuoc), "idthuoc", "tenthuoc");
                return View();
           
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themThuoc(FormCollection collection, ChiTietThuoc ctThuoc)
        {
            var id = collection["id"];
            int[] ten = new int[6];
            for (int i=0; i<6; i++)
            {
               
                    ten[i] =int.Parse(collection["thuoc"+i+""]);
                
            }

            int[] soluong = new int[6];
            for (int i = 0; i <6; i++)
            {
                try
                {
                    soluong[i] = int.Parse(collection["soluong" + i + ""]);
                }
                catch (Exception ex)
                {
                    soluong[i] = 0;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if(soluong[i] > 0)
                {
                    ctThuoc.idKetQua = int.Parse(id);
                    ctThuoc.idthuoc = ten[i];
                    ctThuoc.soluong = soluong[i];
                    data.ChiTietThuocs.Add(ctThuoc);
                    data.SaveChanges();
                }
            }
            
            return Redirect("/admin/dachsachthuoc/" + id);
        }

        public ActionResult dachsachthuoc(int id)
        {
            var danhsach = from a in data.ChiTietThuocs where a.idKetQua == id select a;


            return View(data.ChiTietThuocs.Where(a => a.idKetQua == id).ToList());

        }

        public ActionResult thongke(int id)
        {
            if (Session["tkBacSi"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            var check = DateTime.Now;
            var year = check.Year;

            DateTime timenext;
            if (id == 2)
            {
                 timenext = DateTime.Parse("" + year + " - " + id + " - 28 19:04:00.000");
            }
            else
            {
                 timenext = DateTime.Parse("" + year + " - " + id + " - 30 19:04:00.000");
            }
            
            DateTime time = DateTime.Parse("" + year + " - " + id + " - 01 00:00:00.000");
           
            ViewBag.today = id;
            ViewBag.next = id + 1;
            ViewBag.before = id - 1;
            ViewBag.vienphi = data.KetQuaKhams.OrderByDescending(a => a.LichKham.ngaydat).Where(a => a.LichKham.ngaydat >= time && a.LichKham.ngaydat < timenext && a.LichKham.tinhTrang == true).Sum(a=>a.tienkham);
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.ngaydat >= time && a.ngaydat< timenext && a.tinhTrang == true).ToList());
            
        }
    }
}