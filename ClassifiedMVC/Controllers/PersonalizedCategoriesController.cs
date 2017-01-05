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
    public class PersonalizedCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalizedCategories
        public ActionResult Index()
        {
            var personalizedCategories = db.PersonalizedCategories.Include(p => p.Category).Include(p => p.User);
            return View(personalizedCategories.ToList());
        }

        // GET: PersonalizedCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalizedCategory personalizedCategory = db.PersonalizedCategories.Find(id);
            if (personalizedCategory == null)
            {
                return HttpNotFound();
            }
            return View(personalizedCategory);
        }

        // GET: PersonalizedCategories/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                ViewBag.UserID = new SelectList(db.Users, "Id", "Email", User.Identity.GetUserName().ToString());
            }
            else
            {
                ViewBag.UserID = new SelectList(db.Users, "Id", "Email");
            }

            PersonalizedCategory pc = new PersonalizedCategory();
            pc.CategoryID = 0;
            pc.Path = "";


            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");

            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "----", Value = "3", Selected = true },
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2"}
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text");


            return View(pc);
        }

        // POST: PersonalizedCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonalizedCategoryID,CategoryID,UserID,PriceMin,PriceMax,State,Path")] PersonalizedCategory personalizedCategory, Location l)
        {
            if (ModelState.IsValid)
            {
                if(!User.IsInRole("Admin"))
                {
                    personalizedCategory.UserID = User.Identity.GetUserId();
                }
                string pom = db.Categories.Where(p => p.CategoryID == personalizedCategory.CategoryID).Select(p => p.Name).First().ToString();
                personalizedCategory.Path = categoryPath(pom);

                bool contactExists = db.PersonalizedCategories.Any(p => p.Path == personalizedCategory.Path && p.UserID==personalizedCategory.UserID);
                if (contactExists) // jesli wczesniej istnial
                {
                    ViewBag.ErrorP = "You already have Favourite Category '" + personalizedCategory.Path + "' in your Collection!";
                    ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", personalizedCategory.CategoryID);
                    ViewBag.UserID = new SelectList(db.Users, "Id", "Email", personalizedCategory.UserID);

                    IEnumerable<SelectListItem> lss2;
                    List<SelectListItem> ls2 = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2"},
                    new SelectListItem() { Text = "----", Value = "3", Selected = true }
                };
                    lss2 = ls2;
                    ViewBag.State = new SelectList(lss2, "Text", "Text");

                    return View(personalizedCategory);
                }
                
                db.PersonalizedCategories.Add(personalizedCategory);
                db.SaveChanges();



                #region adding classifiedLocation
                PCL pcl = new PCL();
                pcl.PersonalizedCategoryID = personalizedCategory.PersonalizedCategoryID;
                if(l.LocationName!="----")
                {
                    string lid = db.Locations.Where(p => p.LocationName == l.LocationName).Select(p => p.LocationID).First().ToString();
                    pcl.LocationID = Int32.Parse(lid);
                    db.PCLs.Add(pcl);
                    db.SaveChanges();
                }
                #endregion



                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("MyCategories", "Home");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", personalizedCategory.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", personalizedCategory.UserID);

            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2"},
                    new SelectListItem() { Text = "----", Value = "3", Selected = true }
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text");



            return View(personalizedCategory);
        }

        // GET: PersonalizedCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalizedCategory personalizedCategory = db.PersonalizedCategories.Find(id);
            if (personalizedCategory == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.GetUserId().Equals((personalizedCategory.User.Id)) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", personalizedCategory.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", personalizedCategory.UserID);

            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2" },
                    new SelectListItem() { Text = "----", Value = "3" }
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text", personalizedCategory.State);

            return View(personalizedCategory);
        }

        // POST: PersonalizedCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonalizedCategoryID,CategoryID,UserID,PriceMin,PriceMax,State,Path")] PersonalizedCategory personalizedCategory, Location l)
        {
            if (ModelState.IsValid)
            {
                string pom = db.Categories.Where(p => p.CategoryID == personalizedCategory.CategoryID).Select(p => p.Name).First().ToString();
                personalizedCategory.Path = categoryPath(pom);
                db.Entry(personalizedCategory).State = EntityState.Modified;
                db.SaveChanges();


                #region editing classifiedLocation
                
                if (l.LocationName != "----") // jesli wybrano loc
                {
                    PCL pcl;                   
                    string lid = db.Locations.Where(p => p.LocationName == l.LocationName).Select(p => p.LocationID).First().ToString();

                    bool contactExists = db.PCLs.Any(p => p.PersonalizedCategoryID == personalizedCategory.PersonalizedCategoryID);
                    if (contactExists) // jesli wczesniej istnial
                    {
                        pcl = db.PCLs.Where(p => p.PersonalizedCategoryID == personalizedCategory.PersonalizedCategoryID).First();
                        pcl.LocationID = Int32.Parse(lid);
                        db.SaveChanges();
                    }
                    else
                    {
                        pcl = new PCL();
                        pcl.PersonalizedCategoryID = personalizedCategory.PersonalizedCategoryID;
                        pcl.LocationID = Int32.Parse(lid);
                        db.PCLs.Add(pcl);
                        db.SaveChanges();
                    }
                }
                else
                {
                    bool contactExists = db.PCLs.Any(p => p.PersonalizedCategoryID == personalizedCategory.PersonalizedCategoryID);
                    if (contactExists) // jesli wczesniej istnial
                    {
                        PCL pcl;
                        pcl = db.PCLs.Where(p => p.PersonalizedCategoryID == personalizedCategory.PersonalizedCategoryID).First();
                        db.PCLs.Remove(pcl);
                        db.SaveChanges();
                    }
                }
                #endregion

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("MyCategories", "Home");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", personalizedCategory.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", personalizedCategory.UserID);
            return View(personalizedCategory);
        }

        // GET: PersonalizedCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           

            PersonalizedCategory personalizedCategory = db.PersonalizedCategories.Find(id);
            if (personalizedCategory == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.GetUserId().Equals(personalizedCategory.User.Id) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }

            return View(personalizedCategory);
        }

        // POST: PersonalizedCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalizedCategory personalizedCategory = db.PersonalizedCategories.Find(id);
            db.PersonalizedCategories.Remove(personalizedCategory);
            db.SaveChanges();
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }


            return RedirectToAction("MyCategories", "Home");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public PartialViewResult GenerateLocations(int? id)
        {
            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>();


            var lsx = db.Locations.ToList();

            ls.Add(new SelectListItem() { Text = "----", Value = "0" });
            foreach (var l in lsx)
            {
                ls.Add(new SelectListItem() { Text = l.LocationName, Value = l.LocationID.ToString() });
            }
            


            lss = ls;
            if(id == null)
            {
                ViewBag.LocationName = new SelectList(lss, "Text", "Text", "----");
            }
            else
            {
                bool contactExists = db.PCLs.Any(p => p.PersonalizedCategoryID == id);
                if(contactExists)
                {
                    var x = db.PCLs.Where(p => p.PersonalizedCategoryID == id).First();
                    ViewBag.LocationName = new SelectList(lss, "Text", "Text", x.Location.LocationName);
                }
                else
                {
                    ViewBag.LocationName = new SelectList(lss, "Text", "Text", "----");
                }
            }
            

            return PartialView("GenerateLocations");
        }

        public string categoryPath(string x)
        {
            List<string> ls = new List<string>();

            Category c = db.Categories.Where(q => q.Name == x).First();
            ls.Add(c.Name);
            while (c.CategoryFatherID != null)
            {
                c = c.CategoryFather;
                ls.Add(c.Name);
            }

            ls.Reverse();

            string p = "";
            foreach (var i in ls)
            {
                p += i + " >> ";
            }

            p = p.Remove(p.Length - 4);

            return p;
        }
    }
}
