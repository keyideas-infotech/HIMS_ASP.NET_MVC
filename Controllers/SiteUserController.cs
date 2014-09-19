using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIMS.Models;
using MvcPaging;

namespace HIMS.Areas.Admin.Controllers
{
    public class SiteUserController : Controller
    {
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);
        private DatabaseContext db = new DatabaseContext();

        public ViewResultBase Search(string q, int? page)
        {
            IQueryable<SiteUser> user = db.Users;
            if (!string.IsNullOrEmpty(q))
            {
                if (q.Length == 1)
                {
                    ViewBag.LetraAlfabetica = q;
                    user = user.Where(c => c.USER_NAME.StartsWith(q));
                }
                else if (q.Length > 1)
                {
                    user = user.Where(c => c.USER_NAME.IndexOf(q) > -1);
                }
            }
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var ListPaged = user.OrderBy(i => i.USER_NAME).ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("Index", ListPaged);
            else
                return View("Index", ListPaged);
        }

        public ViewResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Users.OrderBy(c => c.USER_NAME).ToPagedList(currentPageIndex, defaultPageSize));
        }

        public ActionResult Details(int id = 0)
        {
            SiteUser user = db.Users.Find(id);
            return View(user);
        }

        public ActionResult Create()
        {
            SiteUser user = new SiteUser();
            user.UserTitleList = EnumHelper.GetSelectList<enUserTitle>(); 

            return View("CreateOrEdit", user);
        }


        [HttpPost]
        public ActionResult Create(SiteUser user)
        {
            if (ModelState.IsValid)
            {
                int counter = db.Users.Where(c => (c.USER_NUMBER == user.USER_NUMBER && c.USER_ID != user.USER_ID) || (c.EMAILID == user.EMAILID && c.USER_ID != user.USER_ID)).Count();
                if (counter == 0)
                {
                    user.LOGIN_DATE = System.DateTime.Now;
                    user.CREATION_DATE = System.DateTime.Now;
                    user.MODIFY_DATE = System.DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User ID & Email must be unique");
                }
            }
            user.UserTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", user);
        }


        public ActionResult Edit(int id = 0)
        {
            SiteUser user = db.Users.Find(id);
            user.UserTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", user);
        }

        [HttpPost]
        public ActionResult Edit(SiteUser user)
        {
            if (ModelState.IsValid)
            {
                int counter = db.Users.Where(c => (c.USER_NUMBER == user.USER_NUMBER && c.USER_ID != user.USER_ID) || (c.EMAILID == user.EMAILID && c.USER_ID != user.USER_ID)).Count();
                if (counter == 0)
                {
                    if(user.LOGIN_DATE == null)
                        user.LOGIN_DATE = System.DateTime.Now;
                    if (user.CREATION_DATE == null)
                        user.CREATION_DATE = System.DateTime.Now;
                    user.MODIFY_DATE = System.DateTime.Now;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User ID & Email must be unique");
                }
            }
            user.UserTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", user);
        }


        public ActionResult Delete(int id = 0)
        {
            SiteUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteUser user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}