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
    public class UserBusinessUnitController : Controller
    {
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);
        private DatabaseContext db = new DatabaseContext();

        public ViewResultBase Search(string q, int? page)
        {
            IQueryable<UserBusinessUnit> ubUnit = db.UserBusinessUnits;
            if (!string.IsNullOrEmpty(q))
            {
                if (q.Length == 1)
                {
                    ViewBag.LetraAlfabetica = q;
                    ubUnit = ubUnit.Where(c => c.USER_NAME.StartsWith(q));
                }
                else if (q.Length > 1)
                {
                    ubUnit = ubUnit.Where(c => c.USER_NAME.IndexOf(q) > -1);
                }
            }
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var ListPaged = ubUnit.OrderBy(i => i.USER_NAME).Include(r => r.siteUser).Include(r => r.businessUnit).ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("Index", ListPaged);
            else
                return View("Index", ListPaged);
        }

        public ViewResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.UserBusinessUnits.OrderBy(c => c.USER_NAME).Include(r => r.siteUser).Include(r => r.businessUnit).ToPagedList(currentPageIndex, defaultPageSize));
        }

        public ActionResult Details(int id = 0)
        {
            UserBusinessUnit ubUnit = db.UserBusinessUnits.Find(id);
            if (ubUnit != null)
            {
                var user = db.Users.Where(u => u.USER_ID == ubUnit.USER_ID).FirstOrDefault();                
                var bus = db.BusinessUnits.Where(u => u.BUS_ID == ubUnit.BUS_ID).FirstOrDefault();
                if (bus != null)
                    ViewData["BusinessUnit"] = bus.BUSINESS_UNIT;
                else
                    ViewData["BusinessUnit"] = "";
                if (user != null)
                    ViewData["UserNumber"] = user.USER_NUMBER;
                else
                    ViewData["UserNumber"] = "";

                Dictionary<string, string> selList = EnumHelper.GetDictionaryList<enUserType>();
                string strUserType = "";
                selList.TryGetValue(ubUnit.USER_TYPE.ToString(), out strUserType);                  
                ViewData["UserType"]=strUserType;
            }
            else
            {
                ViewData["BusinessUnit"] = "";
                ViewData["UserNumber"] = "";    
                ViewData["UserType"] = "";
            }
            
            return View(ubUnit);
        }

        public ActionResult Create()
        {
            UserBusinessUnit ubUnit = new UserBusinessUnit();
            ubUnit.UserTypeList = EnumHelper.GetSelectList<enUserType>(); 
            ubUnit.UserList = db.Users;
            ubUnit.BusinessUnitList = db.BusinessUnits;
            return View("CreateOrEdit", ubUnit);
        }


        [HttpPost]
        public ActionResult Create(UserBusinessUnit ubUnit)
        {
            if (ModelState.IsValid)
            {
                int counter = db.UserBusinessUnits.Where(c => c.USER_ID == ubUnit.USER_ID && c.BUS_ID == ubUnit.BUS_ID && c.USER_BUS_ID != ubUnit.USER_BUS_ID).Count();
                if (counter == 0)
                {                    
                    ubUnit.CREATION_DATE = System.DateTime.Now;
                    db.UserBusinessUnits.Add(ubUnit);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Name with Business unit must be unique");
                }
            }            
            ubUnit.UserTypeList = EnumHelper.GetSelectList<enUserType>(); 
            ubUnit.UserList = db.Users;
            ubUnit.BusinessUnitList = db.BusinessUnits;
            return View("CreateOrEdit", ubUnit);
        }


        public ActionResult Edit(int id = 0)
        {
            UserBusinessUnit ubUnit = db.UserBusinessUnits.Find(id);            
            ubUnit.UserTypeList = EnumHelper.GetSelectList<enUserType>(); 
            ubUnit.UserList = db.Users;
            ubUnit.BusinessUnitList = db.BusinessUnits;
            return View("CreateOrEdit", ubUnit);
        }

        [HttpPost]
        public ActionResult Edit(UserBusinessUnit ubUnit)
        {
            if (ModelState.IsValid)
            {
                int counter = db.UserBusinessUnits.Where(c => c.USER_ID == ubUnit.USER_ID && c.BUS_ID == ubUnit.BUS_ID && c.USER_BUS_ID != ubUnit.USER_BUS_ID).Count();
                if (counter == 0)
                {
                    if (ubUnit.CREATION_DATE == null)
                        ubUnit.CREATION_DATE = System.DateTime.Now;
                    db.Entry(ubUnit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Name with Business unit must be unique");
                }
            }
            ubUnit.UserTypeList = EnumHelper.GetSelectList<enUserType>(); 
            ubUnit.UserList = db.Users;
            ubUnit.BusinessUnitList = db.BusinessUnits;
            return View("CreateOrEdit", ubUnit);
        }


        public ActionResult Delete(int id = 0)
        {
            UserBusinessUnit ubUnit = db.UserBusinessUnits.Find(id);
            db.UserBusinessUnits.Remove(ubUnit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserBusinessUnit ubUnit = db.UserBusinessUnits.Find(id);
            db.UserBusinessUnits.Remove(ubUnit);
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