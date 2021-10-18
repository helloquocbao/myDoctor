using myDoctor.Models;
using System;
using System.Collections.Generic;
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
            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == accountcheck.idKhachHang).ToList());
            ;
        }

        public ActionResult XemKetQua(int id)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            var chitet = from s in data.KetQuaKhams where s.idDatLich == id select s;

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

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addthannhan(FormCollection collection, chitietThanNhan ct)
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("login", "home");
            }
            KhachHang accountcheck = Session["taikhoan"] as KhachHang;
            var idkh = accountcheck.idKhachHang;
            var sdt = collection["sdt"];
            var mqh = collection["mqh"];
            var check = from s in data.ThanNhans where s.sdtThanNhan == sdt select s;
            if (check == null || sdt == null)
            {
                ViewData["Loi0"] = "Số điện thoại không tồn tại trong hệ thống, vui lòng nhập số khác";

                return RedirectToAction("thannhan", "user");
            }
            else
            {
                try
                {
                    String connet = "Data Source=QUOCBAO\\BAO;Initial Catalog=myDoctor;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connet);
                    connection.Open();
                    String query = "INSERT dbo.chitietThanNhan VALUES ('" + sdt + "'," + idkh + ", N'" + mqh + "')";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ViewData["Loi0"] = "Số điện thoại không tồn tại trong hệ thống, vui lòng nhập số khác";
                    return RedirectToAction("thannhan");
                }
                catch (Exception ex)
                {
                    ViewData["loi1"] = "Số điện thoại trong tồn tại trong hệ thống";
                    return RedirectToAction("thannhan", "user");
                }
            }
            return PartialView();
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

            return View(data.LichKhams.OrderByDescending(a => a.ngaydat).Where(a => a.idKhachHang == id).ToList());
        }
    }
}
