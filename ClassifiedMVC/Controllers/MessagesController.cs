using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassifiedMVC.Models;
using Microsoft.AspNet.Identity;

namespace ClassifiedMVC.Controllers
{
    [Authorize(Roles ="User")]
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Receiver).Include(m => m.Sender);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(string rid)
        {
            Message m = new Message();
            if (rid != null)
            {             
                m.ReceiverID = rid;
                m.Receiver = db.Users.Find(rid);
                m.SenderID = User.Identity.GetUserId();
            }
            //ViewBag.ReceiverID = new SelectList(db.Users, "Id", "Email");
            //ViewBag.SenderID = new SelectList(db.Users, "Id", "Email");
            return View(m);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,SenderID,ReceiverID,Text,Date,Read")] Message message)
        {
            message.Date = DateTime.Now;
            message.Read = false;
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Index", "Classifieds");
            }

            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "Email", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "Id", "Email", message.SenderID);
            return View(message);
        }

        // GET: Messages/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "Email", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "Id", "Email", message.SenderID);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,SenderID,ReceiverID,Text,Date,Read")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "Email", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "Id", "Email", message.SenderID);
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
