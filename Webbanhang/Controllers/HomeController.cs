using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace Webbanhang.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        QLbanhangEntities db = new QLbanhangEntities();
        public ActionResult Index()
        {
            //điện thoại mới
            var lstDTM = db.sanphams.Where(n => n.MaLoai == 1 && n.Moi == 1);
            //gán vào viewbag
            ViewBag.listDTM = lstDTM;

            //tablet mới
            var lstTBM = db.sanphams.Where(n => n.MaLoai == 3 && n.Moi == 1);
            //gán vào viewbag
            ViewBag.listTBM = lstTBM;

            //laptop mới
            var lstLTM = db.sanphams.Where(n => n.MaLoai == 2 && n.Moi == 1);
            //gán vào viewbag
            ViewBag.listLTM = lstLTM;
            return View();
        }
        
        
        public ActionResult MenuPartial()
        {
            var lstSP = db.sanphams;
            return PartialView(lstSP);
        }

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(thanhvien tv,FormCollection f)
        {
            string pass = f["MatKhau"].ToString();
            string repass = f["confirmPass"].ToString();
            if (repass.Length == 0)
            {
                ViewBag.Err = "Hãy xác nhận mật khẩu";
            }
            //ktra capcha
            if (this.IsCaptchaValid("Mã xác thực không chính xác"))
            {
                if (repass.Length == 0)
                {
                    ViewBag.Err = "Hãy xác nhận mật khẩu";
                }
                else if (pass.Length != repass.Length)
                {
                    ViewBag.Err = "Mật khẩu không khớp";
                }
                else if (pass != repass)
                {
                    ViewBag.Err = "Mật khẩu không khớp";
                }
                else
                {
                    ViewBag.thongbao = "thành công";
                    ViewBag.Err = "";
                    db.thanhviens.Add(tv);
                    db.SaveChanges();
                }

            }
            else
            {
                ViewBag.thongbao = "sai mã";
            }

            return View();
        }

        //xử lý đăng nhập
        [HttpPost]
        public ActionResult Dangnhap(FormCollection f)
        {
            //ktra form dang nhap va mk
            string staikhoan = f["txtuser"].ToString();
            string smatkhau = f["txtpass"].ToString();
            thanhvien tv = db.thanhviens.SingleOrDefault(n => n.TaiKhoan == staikhoan && n.MatKhau == smatkhau);
            if (tv != null)
            {
                var lstRole = db.ThanhVien_Role.Where(n => n.MaTV == tv.MaTV);
                string listRole = "";
                if (lstRole.Count() != 0)
                {
                    foreach (var item in lstRole)
                    {
                        listRole += item.Role.RoleID + ",";
                    }
                    listRole = listRole.Substring(0, listRole.Length - 1);
                    PhanQuyen(tv.TaiKhoan.ToString(), listRole);
                }
                Session["taikhoan"] = tv.HoTen;
                return Content("<script>window.location.reload()</script>");
            }
            return Content("Tên tài khoản hoặc mật khẩu không chính xác!");
        }

        public void PhanQuyen(string taikhoan, string Role)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, 
                                                        taikhoan, 
                                                        DateTime.Now, 
                                                        DateTime.Now.AddHours(3), 
                                                        false, 
                                                        Role,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

        public ActionResult Dangxuat(FormCollection f) {
            Session["taikhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}