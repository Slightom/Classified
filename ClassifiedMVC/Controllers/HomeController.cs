using ClassifiedMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ClassifiedMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {


            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public PartialViewResult CategoryChoosingPartial(int? id)
        {
            ICollection<Category> children, children2;
            Dictionary<string, ICollection<Category>> Children = new Dictionary<string, ICollection<Category>>();

            var query = db.Categories.Where(p => p.CategoryFatherID == null).ToList();

            foreach (var i in query)
            {
                children = hasChildren(i.CategoryID);
                Children[i.Name] = null;
                if (children != null)
                {
                    Children[i.Name] = children;
                    foreach (var j in children)
                    {
                        children2 = hasChildren(j.CategoryID);
                        Children[j.Name] = null;
                        if (children2 != null)
                        {
                            Children[j.Name] = children2;
                        }
                    }
                }
            }

            ViewBag.Children = Children;


            if(id != null)
            {
                Classified c = db.Classifieds.Find(id);
                ViewBag.cPath = c.CategoryPath;
                ViewBag.cn = c.Category.Name;
            }
            

            return PartialView("CategoryChoosingPartial", query);
        }

        public ICollection<Category> hasChildren(int cid)
        {
            var query = db.Categories.Where(p => p.CategoryFatherID == cid);

            if (query.Count() > 0) { return query.ToList(); }
            else { return null; }
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



        public PartialViewResult GenerateAllAttributes(int? id)
        {
            AllAtributesModel aam = new AllAtributesModel();
            aam.Engine_power = 100;
            aam.Enginie_capacity = 1.9;
            aam.Mileage = 1000000;
            aam.Number_of_pages = 100;
            aam.Year = 2017;
            aam.Publication_Year = 2017;


                ViewBag.fuel = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 1), "Value", "Value");
                ViewBag.Body_type = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 4), "Value", "Value");
                ViewBag.Transmission = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 5), "Value", "Value");
                ViewBag.Country_of_origin = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 6), "Value", "Value");
                ViewBag.Color = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 9), "Value", "Value");


            if(id != null)
            {
                Classified c = db.Classifieds.Find(id);
                var list = giveMeSelectedAttributes2(c.Category.Name);

                foreach (string s in list)
                {
                    string ss = whiteSpacesAgain(s);
                    ClassifiedAttribute ca = db.ClassifiedAttributes.Where(p => p.ClassifiedID == c.ClassifiedID && p.Attribute.Name == ss).First();

                    switch (s)
                    {
                        case "fuel": aam.fuel = ca.Value; ViewBag.fuel = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 1), "Value", "Value", aam.fuel);  break;
                        case "Engine_power": aam.Engine_power = Double.Parse(ca.Value); break;
                        case "Engine_capacity": aam.Enginie_capacity = Double.Parse(ca.Value); break;
                        case "Body_type": aam.Body_type = ca.Value; ViewBag.Body_type = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 4), "Value", "Value", aam.Body_type); break;
                        case "Transmission": aam.Transmission = ca.Value; ViewBag.Transmission = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 5), "Value", "Value", aam.Transmission); break;
                        case "Country_of_origin": aam.Country_of_origin = ca.Value; ViewBag.Country_of_origin = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 6), "Value", "Value", aam.Country_of_origin); break;
                        case "Mileage": aam.Mileage = Double.Parse(ca.Value); break;
                        case "Year": aam.Year = Int32.Parse(ca.Value); break;
                        case "Color": aam.Color = ca.Value; ViewBag.Color = new SelectList(db.AttributeValues.Where(p => p.AttributeID == 9), "Value", "Value", aam.Color); break;
                        case "Number_of_pages": aam.Number_of_pages = Int32.Parse(ca.Value); break;
                        case "Publication_Year": aam.Publication_Year = Int32.Parse(ca.Value); break;
                    }
                }

                ViewBag.edit = "true";
            }

            return PartialView("GenerateAllAttributes", aam);
        }



        public JsonResult giveMeSelectedAttributes(string x)
        {
            var qu = db.Categories.Select(p => p.Name).ToList();
            ViewBag.Categories = qu;
            


            var ls = new List<string>();
            Category c = db.Categories.Where(q => q.Name == x).First();


            while (c != null)
            {
                var query = db.CategoryAttributes.Where(p => p.CategoryID == c.CategoryID).Select(p => p.Attribute.Name).ToList();
                foreach (var i in query)
                {
                    ls.Add(whiteSpacesLess(i));
                }
                c = c.CategoryFather;
            }

            ls = ls.Distinct().ToList<string>();

            return Json(ls);
        }


        public List<string> giveMeSelectedAttributes2(string x)
        {
            var qu = db.Categories.Select(p => p.Name).ToList();
            ViewBag.Categories = qu;



            var ls = new List<string>();
            Category c = db.Categories.Where(q => q.Name == x).First();


            while (c != null)
            {
                var query = db.CategoryAttributes.Where(p => p.CategoryID == c.CategoryID).Select(p => p.Attribute.Name).ToList();
                foreach (var i in query)
                {
                    ls.Add(whiteSpacesLess(i));
                }
                c = c.CategoryFather;
            }

            ls = ls.Distinct().ToList<string>();

            return ls;
        }

        public static string whiteSpacesLess(string x)
        {
            x = x.Replace(" ", "_");
            return x;
        }

        public static string whiteSpacesAgain(string x)
        {
            x = x.Replace("_", " ");
            return x;
        }


        public JsonResult giveMeAllAttributes(string x)
        {

            var ls = new List<string>();
            var query = db.Attributes.Select(p => p.Name).ToList();

            foreach(var i in query)
            {
                ls.Add(whiteSpacesLess(i));
            }

            ls = ls.Distinct().ToList<string>();


            return Json(ls);
        }


        public int CategoryID(string x)
        {
            string id = db.Categories.Where(p => p.Name == x).Select(p => p.CategoryID).First().ToString();
            int cid = Int32.Parse(id);

            return cid;
        }



        public PartialViewResult GenerateLocations()
        {
            ViewBag.LocationName = new SelectList(db.Locations.OrderBy(p=>p.LocationName), "LocationName", "LocationName");

            Location l = new Location();

            return PartialView("GenerateLocations");
        }



        [Authorize(Roles = "User")]
        public ActionResult MyMessages()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult MyProfile()
        {
            string uid = User.Identity.GetUserId();

            User u = db.Users.Find(uid);

            ViewBag.classifiedCount = db.Classifieds.Where(p => p.UserID == uid).Count();

            return View(u);
        }

        [Authorize(Roles = "User")]
        public PartialViewResult sendReceivedMessages()
        {
            string id = User.Identity.GetUserId();

            var messagesReceived = db.Messages.Where(p => p.ReceiverID == id).OrderByDescending(p => p.Date).ToList();
            var messagesSend = db.Messages.Where(p => p.SenderID == id).OrderByDescending(p => p.Date).ToList();

            ViewBag.messagesSend = messagesSend;

            return PartialView("sendReceivedMessages", messagesReceived);
        }

        public string readBefore(int id)
        {
            Message m = db.Messages.Where(p => p.MessageID == id).FirstOrDefault();

            if (m.Read == false)
            {
                m.Read = true;
                db.SaveChanges();
                return "false";
            }

            return "true";
        }


        public ActionResult SendMessage(string id)

        {
            string idS = User.Identity.GetUserId();

            Message m = new Message();
            m.SenderID = idS;
            m.ReceiverID = id;
            m.Receiver = db.Users.Find(id);

            ViewBag.ReceiverID = new SelectList(db.Users, "Id", "UserName", id);
            ViewBag.SenderID = new SelectList(db.Users, "Id", "UserName", idS);

            return View(m);
        }

        public int howManyNms()
        {
            if (User.Identity.GetUserId() != null)
            {
                string id = User.Identity.GetUserId();
                int ile = db.Messages.Where(p => p.ReceiverID == id && p.Read == false).Count();

                return ile;
            }

            return -1;
        }

        public static int howManyNms2(string xid)
        {
            ApplicationDbContext db2 = new ApplicationDbContext();

            int ile = db2.Messages.Where(p => p.ReceiverID == xid && p.Read == false).Count();

            return ile;
        }


        public string isActiceAdminMessage()
        {
            var am = db.AdminMessages.ToList();

            foreach(var a in am)
            {
                if(a.Active == true)
                { return a.Text; }
            }
            return "";
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage([Bind(Include = "MessageID,Date,SenderID,ReceiverID,Text")] Message message)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = ClassifiedsController.consistBadWord(message.Text);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                message.Date = DateTime.Now;

                db.Messages.Add(message);
                db.SaveChanges();

                return RedirectToAction("Mymessages", "Home");



            }

            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick", message.SenderID);
            return View(message);
        }
    }
}