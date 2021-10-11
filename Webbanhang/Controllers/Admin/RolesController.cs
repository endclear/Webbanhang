using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Models;

namespace Webbanhang.Controllers.Admin
{
    [Authorize(Roles = "Developer")]
    public class RolesController : Controller
    {
        QLbanhangEntities db = new QLbanhangEntities();
        // GET: Roles
        public ActionResult Index()
        {
            try
            {
                var lstRoles = db.Roles;
                return View(lstRoles);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Role _role)
        {
            try {
                db.Roles.Add(_role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return HttpNotFound(ex.ToString());
            }
        }

        public Boolean checkRole(string RoleID)
        {
            Role _role = db.Roles.SingleOrDefault(n => n.RoleID == RoleID);
            if (_role != null)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public ActionResult Edit(string RoleID)
        {
            Role _role = db.Roles.SingleOrDefault(n => n.RoleID == RoleID.ToString());
            if (_role == null)
            {
                return HttpNotFound();
            }
            return View(_role);
        }

        [HttpPost]
        public ActionResult Edit(Role _role)
        {
            db.Entry(_role).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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