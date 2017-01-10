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
using System.IO;
using PagedList;

namespace ClassifiedMVC.Controllers
{
    public class ClassifiedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GenerateSearchModel(SearchModel model)
        {
            IEnumerable<SelectListItem> lss0;
            List<SelectListItem> ls0 = new List<SelectListItem>();

            var lsx0 = db.Categories.ToList();

            ls0.Add(new SelectListItem() { Text = "----", Value = "0" });
            foreach (var l in lsx0)
            {
                ls0.Add(new SelectListItem() { Text = l.Name, Value = l.CategoryID.ToString() });
            }



            lss0 = ls0;

            ViewBag.CategoryID = new SelectList(lss0, "Text", "Text", "----");
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "----", Value = "3", Selected = true },
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2"}
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text");
            /////////////////////////////////////////////////////////////////////////////////////////////////




            IEnumerable<SelectListItem> lss2;
            List<SelectListItem> ls2 = new List<SelectListItem>();

            var lsx = db.Locations.ToList();

            ls2.Add(new SelectListItem() { Text = "----", Value = "----" });
            foreach (var l in lsx)
            {
                ls2.Add(new SelectListItem() { Text = l.LocationName, Value = l.LocationName });
            }



            lss2 = ls2;

            ViewBag.Location = new SelectList(lss2, "Text", "Text", "----");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            SearchModel sm = new SearchModel();
            if (model != null)
            {
                if(model.Location!=null)
                {
                    ViewBag.Location = new SelectList(lss2, "Text", "Text", model.Location);
                }
                else
                {
                    ViewBag.Location = new SelectList(lss2, "Text", "Text", "----");
                }

                ViewBag.State = new SelectList(lss, "Text", "Text", model.State);
                if(model.CategoryID!=0)
                {
                    Category c = db.Categories.Find(model.CategoryID);
                    ViewBag.CategoryID = new SelectList(lss0, "Value", "Text", c.CategoryID.ToString());
                }
                else
                {
                    ViewBag.CategoryID = new SelectList(lss0, "Value", "Text", "----");
                }
                sm.PriceMin = model.PriceMin;
                sm.PriceMax = model.PriceMax;
            }

            else
            {
                sm.CategoryID = 0;
                sm.PriceMax = 1000000;
            }
            

            return View(sm);
        }





        public ActionResult Index(string searchString, int? page, bool? myClassified, bool? ReportedOnly, SearchModel sm)
        {
            var classifieds = db.Classifieds.Include(c => c.Category).Include(c => c.User);

            if (!String.IsNullOrEmpty(searchString)) // jeśli jest wyszukiwanie, to wybierz odpowiednie
            {
                ViewBag.searchString = searchString;
                classifieds = classifieds.Where(p => p.Name.Contains(searchString) || p.CategoryPath.Contains(searchString));
            }

            if (myClassified != null) // jeśli jest moje ogłoszenia
            {
                //ViewBag.searchString = "myClassified";
                string uid = User.Identity.GetUserId();
                classifieds = classifieds.Where(p => p.UserID == uid);
            }

            if (ReportedOnly != null) // jeśli jest Reported Only
            {
                //ViewBag.searchString = "myClassified";
                classifieds = classifieds.Where(p => p.Reported != null && p.Reported != "");
            }


            if (searchString != null)
            {
                ViewBag.Filters = "unset;";
                classifieds = classifieds.Where(p => p.Price >= sm.PriceMin && p.Price <= sm.PriceMax);
                if (sm.CategoryID != 0)
                {
                    Category c = db.Categories.Find(sm.CategoryID);
                    classifieds = classifieds.Where(p => p.CategoryPath.Contains(c.Name));
                }

                if (sm.State != "----")
                {
                    classifieds = classifieds.Where(p => p.State == sm.State);
                }

                if (sm.Location != "----")
                {
                    List<Classified> allgood = new List<Classified>();
                    ApplicationDbContext db22 = new ApplicationDbContext();
                    foreach (var c in classifieds)
                    {
                        ClassifiedLocation cl = db22.ClassifiedLocations.Where(p => p.ClassifiedID == c.ClassifiedID).First();
                        if (cl.Location.LocationName.Contains(sm.Location))
                        {
                            allgood.Add(c);
                        }
                    }

                    classifieds = allgood.AsQueryable();
                }

                ViewBag.SM = sm;
            }

            else { ViewBag.Filters = "none;"; }




            classifieds = classifieds.OrderByDescending(p => p.DateAdded);



            var location = new Dictionary<int, string>();


            ApplicationDbContext db2 = new ApplicationDbContext();
            foreach (var c in classifieds)
            {
                string lid = db2.ClassifiedLocations.Where(p => p.ClassifiedID == c.ClassifiedID).Select(p => p.LocationID).First().ToString();
                int lidi = Int32.Parse(lid);
                Location l = db2.Locations.Find(lidi);
                location.Add(c.ClassifiedID, l.LocationName);
            }
            ViewBag.loc = location;


            int userPagination = -1;
            string uid2 = User.Identity.GetUserId();
            User u = db.Users.Find(uid2);
            if (u != null && u.Pagination > 0)
            {
                userPagination = u.Pagination;
            }
            else
            {
                userPagination = 5;
            }


            return View(classifieds.ToList().ToPagedList(page ?? 1, userPagination));
        }

        #region details

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

            classified.Counter += 1;
            db.SaveChanges();

            ViewBag.UserId = User.Identity.GetUserId();

            return View(classified);
        }

        #endregion

        #region create

        [Authorize(Roles = "User")]

        // GET: Classifieds/Create
        public ActionResult Create()
        {
            Classified c = new Classified();
            c.CategoryID = 0;
            c.CategoryPath = "";
            ViewBag.CategoryID = new SelectList(db.Categories.Where(p => p.CategoryFatherID == null), "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1", Selected = true },
                    new SelectListItem() { Text = "Used", Value = "2" }
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text");
            return View(c);
        }

        // POST: Classifieds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassifiedID,UserID,Name,Description,CategoryID,DateAdded,Price,State,Counter,Reported")] Classified classified, AllAtributesModel aam, Location l, HttpPostedFileBase upload0, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3)
        {

            if (ModelState.IsValid && classified.CategoryID != 0)
            {
                classified.DateAdded = DateTime.Now;
                classified.UserID = User.Identity.GetUserId();

                ViewBag.badWord = consistBadWord(classified.Description);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                string pom = db.Categories.Where(p => p.CategoryID == classified.CategoryID).Select(p => p.Name).First().ToString();
                classified.CategoryPath = categoryPath(pom);

                db.Classifieds.Add(classified);
                db.SaveChanges();

                #region adding classifiedLocation

                ClassifiedLocation cl = new ClassifiedLocation();
                cl.ClassifiedID = classified.ClassifiedID;
                string lid = db.Locations.Where(p => p.LocationName == l.LocationName).Select(p => p.LocationID).First().ToString();
                cl.LocationID = Int32.Parse(lid);
                db.ClassifiedLocations.Add(cl);
                db.SaveChanges();

                #endregion

                #region adding photos

                if (upload0 != null)
                {
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload0.jpg");
                    //zapis
                    upload0.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload0.jpg";
                    p.MainPhoto = true;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }
                else
                {
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    p.Path = "/Content/photos/default.jpg";
                    p.MainPhoto = true;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                if (upload1 != null)
                {
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;
                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload1.jpg");
                    //zapis
                    upload1.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload1.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                if (upload2 != null)
                {
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload2.jpg");
                    //zapis
                    upload2.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload2.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                if (upload3 != null)
                {
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload3.jpg");
                    //zapis
                    upload3.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload3.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                #endregion

                #region adding classifiedAttributes

                var list = giveMeSelectedAttributes(classified.CategoryID);
                foreach (string s in list)
                {
                    ClassifiedAttribute ca = new ClassifiedAttribute();
                    ca.ClassifiedID = classified.ClassifiedID;
                    string ss = HomeController.whiteSpacesAgain(s);
                    string aid = db.Attributes.Where(p => p.Name == ss).Select(p => p.AttributeID).First().ToString();
                    ca.AttributeID = Int32.Parse(aid);
                    switch (s)
                    {
                        case "fuel": ca.Value = aam.fuel; break;
                        case "Engine_power": ca.Value = aam.Engine_power.ToString(); break;
                        case "Engine_capacity": ca.Value = aam.Enginie_capacity.ToString(); break;
                        case "Body_type": ca.Value = aam.Body_type; break;
                        case "Transmission": ca.Value = aam.Transmission; break;
                        case "Country_of_origin": ca.Value = aam.Country_of_origin; break;
                        case "Mileage": ca.Value = aam.Mileage.ToString(); break;
                        case "Year": ca.Value = aam.Year.ToString(); break;
                        case "Color": ca.Value = aam.Color; break;
                        case "Number_of_pages": ca.Value = aam.Number_of_pages.ToString(); break;
                        case "Publication_Year": ca.Value = aam.Publication_Year.ToString(); break;
                    }

                    db.ClassifiedAttributes.Add(ca);
                    db.SaveChanges();
                }




                #endregion



                return RedirectToAction("Index");
            }


            if (classified.CategoryID == 0)
            {
                ViewBag.Category0 = "You didn't choose Category!";
            }


            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);
            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1", Selected = true },
                    new SelectListItem() { Text = "Used", Value = "2" }
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text");

            return View(classified);
        }

        #endregion

        #region edit
        // GET: Classifieds/Edit/5
        [Authorize(Roles = "User")]
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


            if (!User.Identity.GetUserId().Equals((classified.User.Id)) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);


            IEnumerable<SelectListItem> lss;
            List<SelectListItem> ls = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "New", Value = "1"},
                    new SelectListItem() { Text = "Used", Value = "2" }
                };
            lss = ls;
            ViewBag.State = new SelectList(lss, "Text", "Text", classified.State);

            ViewBag.Edit = "true";

            return View(classified);
        }

        // POST: Classifieds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassifiedID,UserID,Name,Description,CategoryID,DateAdded,Price,State,Counter,Reported")] Classified classified, AllAtributesModel aam, Location l, HttpPostedFileBase upload0, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3)
        {
            if (ModelState.IsValid)
            {
                ViewBag.badWord = consistBadWord(classified.Description);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                string pom = db.Categories.Where(p => p.CategoryID == classified.CategoryID).Select(p => p.Name).First().ToString();
                classified.CategoryPath = categoryPath(pom);

                db.Entry(classified).State = EntityState.Modified;
                db.SaveChanges();





                ClassifiedLocation cl = db.ClassifiedLocations.Where(p => p.ClassifiedID == classified.ClassifiedID).First();
                string lid = db.Locations.Where(p => p.LocationName == l.LocationName).Select(p => p.LocationID).First().ToString();
                cl.LocationID = Int32.Parse(lid);
                db.SaveChanges();




                if (upload0 != null)
                {
                    //remove old
                    var old = findOld(classified.ClassifiedID, "upload0");
                    var photoName = old.Path;
                    string fullPath = Request.MapPath(photoName);

                    if (!old.Path.Equals("default.jpg"))
                    {
                        System.IO.File.Delete(fullPath);

                        db.Photos.Remove(old);
                        db.SaveChanges();
                        //add new

                        Photo p = new Photo();
                        p.ClassifiedID = classified.ClassifiedID;

                        //pelny sciezka do pliku
                        var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload0.jpg");
                        //zapis
                        upload0.SaveAs(path);

                        p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload0.jpg";
                        p.MainPhoto = true;

                        db.Photos.Add(p);
                        db.SaveChanges();
                    }


                    else
                    {
                        //add new

                        //pelny sciezka do pliku
                        var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload0.jpg");
                        //zapis
                        upload0.SaveAs(path);

                        old.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload0.jpg";
                        old.MainPhoto = true;

                        db.SaveChanges();
                    }

                }
                else
                {

                }

                if (upload1 != null)
                {
                    //remove old
                    var old = findOld(classified.ClassifiedID, "upload1");
                    if (old != null)
                    {
                        var photoName = old.Path;
                        string fullPath = Request.MapPath(photoName);

                        System.IO.File.Delete(fullPath);

                        db.Photos.Remove(old);
                        db.SaveChanges();

                        //add new
                    }

                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;
                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload1.jpg");
                    //zapis
                    upload1.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload1.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                if (upload2 != null)
                {
                    //remove old
                    var old = findOld(classified.ClassifiedID, "upload2");
                    if (old != null)
                    {
                        var photoName = old.Path;
                        string fullPath = Request.MapPath(photoName);

                        System.IO.File.Delete(fullPath);

                        db.Photos.Remove(old);
                        db.SaveChanges();

                        //add new
                    }


                    //add new

                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload2.jpg");
                    //zapis
                    upload2.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload2.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }

                if (upload3 != null)
                {
                    //remove old
                    var old = findOld(classified.ClassifiedID, "upload3");
                    if (old != null)
                    {
                        var photoName = old.Path;
                        string fullPath = Request.MapPath(photoName);

                        System.IO.File.Delete(fullPath);

                        db.Photos.Remove(old);
                        db.SaveChanges();

                        //add new
                    }


                    //add new
                    Photo p = new Photo();
                    p.ClassifiedID = classified.ClassifiedID;

                    //pelny sciezka do pliku
                    var path = Path.Combine(Server.MapPath("~/Content/photos"), classified.ClassifiedID.ToString() + "_upload3.jpg");
                    //zapis
                    upload3.SaveAs(path);

                    p.Path = "/Content/photos/" + classified.ClassifiedID.ToString() + "_upload3.jpg";
                    p.MainPhoto = false;

                    db.Photos.Add(p);
                    db.SaveChanges();
                }





                var list2 = db.ClassifiedAttributes.Where(p => p.ClassifiedID == classified.ClassifiedID).ToList();
                foreach (var i in list2)
                {
                    db.ClassifiedAttributes.Remove(i);
                }
                db.SaveChanges();

                var list = giveMeSelectedAttributes(classified.CategoryID);
                foreach (string s in list)
                {
                    ClassifiedAttribute ca = new ClassifiedAttribute();
                    ca.ClassifiedID = classified.ClassifiedID;
                    string ss = HomeController.whiteSpacesAgain(s);
                    string aid = db.Attributes.Where(p => p.Name == ss).Select(p => p.AttributeID).First().ToString();
                    ca.AttributeID = Int32.Parse(aid);
                    switch (s)
                    {
                        case "fuel": ca.Value = aam.fuel; break;
                        case "Engine_power": ca.Value = aam.Engine_power.ToString(); break;
                        case "Engine_capacity": ca.Value = aam.Enginie_capacity.ToString(); break;
                        case "Body_type": ca.Value = aam.Body_type; break;
                        case "Transmission": ca.Value = aam.Transmission; break;
                        case "Country_of_origin": ca.Value = aam.Country_of_origin; break;
                        case "Mileage": ca.Value = aam.Mileage.ToString(); break;
                        case "Year": ca.Value = aam.Year.ToString(); break;
                        case "Color": ca.Value = aam.Color; break;
                        case "Number_of_pages": ca.Value = aam.Number_of_pages.ToString(); break;
                        case "Publication_Year": ca.Value = aam.Publication_Year.ToString(); break;
                    }

                    db.ClassifiedAttributes.Add(ca);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", classified.UserID);
            return View(classified);
        }


        #endregion

        #region delete
        // GET: Classifieds/Delete/5
        [Authorize(Roles = "User")]
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


            if (!User.Identity.GetUserId().Equals((classified.User.Id)) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }


            return View(classified);
        }

        // POST: Classifieds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classified classified = db.Classifieds.Find(id);


            foreach (var p in classified.Photos)
            {
                var photoName = p.Path;
                string fullPath = Request.MapPath(photoName);

                if (System.IO.File.Exists(fullPath))
                {
                    if (!photoName.Equals("/Content/photos/default.jpg"))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }


            db.Classifieds.Remove(classified);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        #endregion

        public ActionResult ClearViews(int id)
        {
            Classified c = db.Classifieds.Find(id);
            c.Counter = 0;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = c.ClassifiedID });
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string consistBadWord(string comment)
        {
            ApplicationDbContext dbx = new ApplicationDbContext();
            List<string> badwords = dbx.BannedWords.Select(n => n.Name).ToList();

            foreach (string bw in badwords)
            {
                if (comment.Contains(bw))
                {
                    return bw;
                }
            }

            return "";
        }



        public List<string> giveMeSelectedAttributes(int x)
        {
            var qu = db.Categories.Select(p => p.Name).ToList();



            var ls = new List<string>();
            Category c = db.Categories.Where(q => q.CategoryID == x).First();


            while (c != null)
            {
                var query = db.CategoryAttributes.Where(p => p.CategoryID == c.CategoryID).Select(p => p.Attribute.Name).ToList();
                foreach (var i in query)
                {
                    ls.Add(HomeController.whiteSpacesLess(i));
                }
                c = c.CategoryFather;
            }

            ls = ls.Distinct().ToList<string>();

            return ls;
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


        public PartialViewResult GenerateLocations(string ln)
        {
            ViewBag.LocationName = new SelectList(db.Locations.OrderBy(p => p.LocationName), "LocationName", "LocationName", ln);


            return PartialView("GenerateLocations");
        }


        public PartialViewResult GenerateGallery2(int id)
        {
            Classified c = db.Classifieds.Find(id);

            int x = c.Photos.Count();
            ViewBag.count = x;

            if (x > 0) { ViewBag.p0 = c.Photos.ElementAt(0).Path; }
            if (x > 1) { ViewBag.p1 = c.Photos.ElementAt(1).Path; }
            if (x > 2) { ViewBag.p2 = c.Photos.ElementAt(2).Path; }
            if (x > 3) { ViewBag.p3 = c.Photos.ElementAt(3).Path; }

            return PartialView("GenerateGallery2");
        }


        public Photo findOld(int id, string x)
        {

            if (db.Classifieds.Find(id) == null)
            {
                return null;
            }
            Classified c = db.Classifieds.Find(id);

            List<Photo> photos = db.Photos.Where(a => a.ClassifiedID == id).ToList();
            foreach (var p in photos)
            {
                if (p.Path.Contains(x))
                {
                    return p;
                }
            }

            return null;
        }



        public string ReportAbused(int id, string reportedText)
        {
            Classified c = db.Classifieds.Find(id);

            if (c.Reported == null || c.Reported.Equals(""))
            {
                c.Reported = reportedText;
                db.SaveChanges();
                return "Classified has been Reported!";
            }
            else
            {
                return "Classified had ben reported before!";
            }
        }

        public string ReportedBefore(int id)
        {
            Classified c = db.Classifieds.Find(id);

            if (c.Reported == null || c.Reported.Equals(""))
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public static int howManyReported()
        {
            ApplicationDbContext db2 = new ApplicationDbContext();
            var cs = db2.Classifieds.ToList();
            int ile = 0;

            foreach (var c in cs)
            {
                if (c.Reported != null && c.Reported != "")
                {
                    ile++;
                }
            }

            return ile;
        }
    }
}
