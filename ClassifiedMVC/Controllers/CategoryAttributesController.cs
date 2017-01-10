using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassifiedMVC.Models;

namespace ClassifiedMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryAttributesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoryAttributes
        public ActionResult Index()
        {
            var categoryAttributes = db.CategoryAttributes.Include(c => c.Attribute).Include(c => c.Category);
            return View(categoryAttributes.ToList());
        }

        // GET: CategoryAttributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryAttribute categoryAttribute = db.CategoryAttributes.Find(id);
            if (categoryAttribute == null)
            {
                return HttpNotFound();
            }
            return View(categoryAttribute);
        }

        // GET: CategoryAttributes/Create
        public ActionResult Create()
        {
            ViewBag.AttributeID = new SelectList(db.Attributes, "AttributeID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: CategoryAttributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryAttributeID,CategoryID,AttributeID")] CategoryAttribute categoryAttribute)
        {
            if (ModelState.IsValid)
            {
                db.CategoryAttributes.Add(categoryAttribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeID = new SelectList(db.Attributes, "AttributeID", "Name", categoryAttribute.AttributeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", categoryAttribute.CategoryID);
            return View(categoryAttribute);
        }

        // GET: CategoryAttributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryAttribute categoryAttribute = db.CategoryAttributes.Find(id);
            if (categoryAttribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeID = new SelectList(db.Attributes, "AttributeID", "Name", categoryAttribute.AttributeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", categoryAttribute.CategoryID);
            return View(categoryAttribute);
        }

        // POST: CategoryAttributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryAttributeID,CategoryID,AttributeID")] CategoryAttribute categoryAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryAttribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeID = new SelectList(db.Attributes, "AttributeID", "Name", categoryAttribute.AttributeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", categoryAttribute.CategoryID);
            return View(categoryAttribute);
        }

        // GET: CategoryAttributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryAttribute categoryAttribute = db.CategoryAttributes.Find(id);
            if (categoryAttribute == null)
            {
                return HttpNotFound();
            }
            return View(categoryAttribute);
        }

        // POST: CategoryAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryAttribute categoryAttribute = db.CategoryAttributes.Find(id);
            db.CategoryAttributes.Remove(categoryAttribute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
