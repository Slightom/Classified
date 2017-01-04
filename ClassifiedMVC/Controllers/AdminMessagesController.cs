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
    [Authorize(Roles = "Admin")]
    public class AdminMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminMessages
        public ActionResult Index()
        {
            return View(db.AdminMessages.ToList());
        }

        // GET: AdminMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMessage adminMessage = db.AdminMessages.Find(id);
            if (adminMessage == null)
            {
                return HttpNotFound();
            }
            return View(adminMessage);
        }

        // GET: AdminMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminMessageID,Title,Text,Active")] AdminMessage adminMessage)
        {
            adminMessage.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (adminMessage.Active == true)
                {
                    var am = db.AdminMessages.ToList();
                    foreach(var a in am)
                    {
                        if(a.Active==true)
                        {
                            a.Active = false;
                        }
                    }
                    db.SaveChanges();
                }



                db.AdminMessages.Add(adminMessage);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(adminMessage);
        }

        // GET: AdminMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMessage adminMessage = db.AdminMessages.Find(id);
            if (adminMessage == null)
            {
                return HttpNotFound();
            }
            return View(adminMessage);
        }

        // POST: AdminMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminMessageID,Title,Text,Date,Active")] AdminMessage adminMessage)
        {
            if (ModelState.IsValid)
            {
                if (adminMessage.Active == true)
                {
                    ApplicationDbContext db2 = new ApplicationDbContext();
                    var am = db2.AdminMessages.ToList();
                    foreach (var a in am)
                    {
                        if (a.Active == true)
                        {
                            a.Active = false;
                            db2.SaveChanges();
                        }
                    }
                    
                }



                db.Entry(adminMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminMessage);
        }

        // GET: AdminMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMessage adminMessage = db.AdminMessages.Find(id);
            if (adminMessage == null)
            {
                return HttpNotFound();
            }
            return View(adminMessage);
        }

        // POST: AdminMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminMessage adminMessage = db.AdminMessages.Find(id);
            db.AdminMessages.Remove(adminMessage);
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
