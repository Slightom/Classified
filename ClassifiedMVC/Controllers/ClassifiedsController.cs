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
    public class ClassifiedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classifieds
        public ActionResult Index()
        {
            var classifieds = db.Classifieds.Include(c => c.Category).Include(c => c.User);
            return View(classifieds.ToList());
        }

        // GET: Classifieds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            return View(classified);
        }

        // GET: Classifieds/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Classifieds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassifiedID,UserID,Name,Description,CategoryID,DateAdded,Price,State,Counter,Reported")] Classified classified)
        {
            if (ModelState.IsValid)
            {
                db.Classifieds.Add(classified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);
            return View(classified);
        }

        // GET: Classifieds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);
            return View(classified);
        }

        // POST: Classifieds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassifiedID,UserID,Name,Description,CategoryID,DateAdded,Price,State,Counter,Reported")] Classified classified)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classified).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);
            return View(classified);
        }

        // GET: Classifieds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            return View(classified);
        }

        // POST: Classifieds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classified classified = db.Classifieds.Find(id);
            db.Classifieds.Remove(classified);
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
