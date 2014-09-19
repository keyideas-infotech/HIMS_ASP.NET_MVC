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
    public class BusinessUnitController : Controller
    {
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);
        private DatabaseContext db = new DatabaseContext();

        public ViewResultBase Search(string q, int? page)
        {
            IQueryable<BusinessUnit> businessUnit = db.BusinessUnits;
            if (!string.IsNullOrEmpty(q))
            {
                if (q.Length == 1)
                {
                    ViewBag.LetraAlfabetica = q;
                    businessUnit = businessUnit.Where(c => c.BUSINESS_UNIT.StartsWith(q));
                }
                else if (q.Length > 1)
                {
                    businessUnit = businessUnit.Where(c => c.BUSINESS_UNIT.IndexOf(q) > -1);
                }
            }
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var ListPaged = businessUnit.OrderBy(i => i.BUSINESS_UNIT).ToPagedList(currentPageIndex, defaultPageSize);
            if (Request.IsAjaxRequest())
                return PartialView("Index", ListPaged);
            else
                return View("Index", ListPaged);
        }

        public ViewResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.BusinessUnits.OrderBy(c => c.BUSINESS_UNIT).ToPagedList(currentPageIndex, defaultPageSize));
        } 
        
        public ActionResult Details(int id = 0)
        {
            BusinessUnit businessunit = db.BusinessUnits.Find(id);            
            return View(businessunit);
        }

        public ActionResult Create()
        {
            BusinessUnit bus = new BusinessUnit();
            bus.ContactTitleList = EnumHelper.GetSelectList<enUserTitle>(); 

            return View("CreateOrEdit", bus);
        }
        

        [HttpPost]
        public ActionResult Create(BusinessUnit businessunit)
        {
            if (ModelState.IsValid)
            {
                int counter = db.BusinessUnits.Where(c => c.BUSINESS_UNIT==businessunit.BUSINESS_UNIT && c.BUS_ID!=businessunit.BUS_ID).Count();
                if (counter == 0)
                {
                    businessunit.CREATION_DATE = System.DateTime.Now;
                    businessunit.MODIFY_DATE = System.DateTime.Now;
                    db.BusinessUnits.Add(businessunit);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Business Unit Name must be unique");
                }
            }
            businessunit.ContactTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", businessunit);
        }
        

        public ActionResult Edit(int id = 0)
        {
            BusinessUnit businessunit = db.BusinessUnits.Find(id);
            businessunit.EFFF_DATE = businessunit.EFF_DATE;  
            businessunit.ContactTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", businessunit);
        }        

        [HttpPost]
        public ActionResult Edit(BusinessUnit businessunit)
        {
            if (ModelState.IsValid)
            {
                int counter = db.BusinessUnits.Where(c => c.BUSINESS_UNIT == businessunit.BUSINESS_UNIT && c.BUS_ID != businessunit.BUS_ID).Count();
                if (counter == 0)
                {
                    if (businessunit.CREATION_DATE == null)
                       businessunit.CREATION_DATE = System.DateTime.Now;
                    if (businessunit.EFF_DATE == null)
                        businessunit.EFF_DATE = businessunit.EFFF_DATE;
                    businessunit.MODIFY_DATE = System.DateTime.Now;
                    db.Entry(businessunit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Business Unit Name must be unique");
                }
            }
            businessunit.ContactTitleList = EnumHelper.GetSelectList<enUserTitle>(); 
            return View("CreateOrEdit", businessunit);
        }
        

        public ActionResult Delete(int id = 0)
        {
            BusinessUnit businessunit = db.BusinessUnits.Find(id);
            db.BusinessUnits.Remove(businessunit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessUnit businessunit = db.BusinessUnits.Find(id);
            db.BusinessUnits.Remove(businessunit);
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