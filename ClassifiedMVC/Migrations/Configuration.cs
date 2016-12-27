namespace ClassifiedMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Models.IdentityManager im = new Models.IdentityManager();
            if (!im.RoleExists("Admin")) { im.CreateRole("Admin"); }
            if (!im.RoleExists("User")) { im.CreateRole("User"); }

            var passwordHash = new PasswordHasher();
            User u = new User();
            u.Email = "tomasz.suchwalko@gmail.com";
            u.UserName = "slightom";
            //u.Nick = "slightom";
            u.PasswordHash = passwordHash.HashPassword("Slightomp+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("slightom", "User");

            u = new User();
            u.Email = "witek15@gmail.com";
            u.UserName = "witek15";
            //u.Nick = "witek15";
            u.PasswordHash = passwordHash.HashPassword("Witek15p+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("witek15", "User");

            u = new User();
            u.Email = "wiktor500@gmail.com";
            u.UserName = "wiktor500";
            //u.Nick = "wiktor500";
            u.PasswordHash = passwordHash.HashPassword("Wiktor500p+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("wiktor500", "User");

            u = new User();
            u.Email = "jolkajolkapamietasz@gmail.com";
            u.UserName = "jola17";
            //u.Nick = "jola17";
            u.PasswordHash = passwordHash.HashPassword("Jola17p+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("jola17", "User");

            u = new User();
            u.Email = "admin1@gmail.com";
            u.UserName = "admin1";
            //u.Nick = "admin1";
            u.PasswordHash = passwordHash.HashPassword("Admin1p+");
            u.SecurityStamp = Guid.NewGuid().ToString();
            context.Users.AddOrUpdate(p => p.UserName, u);
            context.SaveChanges();
            im.AddUserToRoleByUsername("admin1", "Admin");






            BannedWord b = new BannedWord();
            b.Name = "krucafuks";
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();

            b = new BannedWord();
            b.Name = "psia koœæ".ToString();
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();

            b = new BannedWord();
            b.Name = "kurka wodna".ToString();
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();

            b = new BannedWord();
            b.Name = "pata³ach".ToString();
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();

            b = new BannedWord();
            b.Name = "motyla noga".ToString();
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();

            b = new BannedWord();
            b.Name = "cholercia".ToString();
            context.BannedWords.AddOrUpdate(p => p.Name, b);
            context.SaveChanges();







            AdminMessage am = new AdminMessage();
            am.Title = "Weso³ych Œwi¹t!";
            am.Text = "Administratorzy witryny MyOLX ¿ycz¹ wszystkim u¿ytkownikom Weso³ych Œwi¹t!";
            am.Date = new DateTime(2016, 12, 20, 10, 10, 10);
            context.AdminMessages.AddOrUpdate(p => p.Title, am);






            string ids, idr;
            ids = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            idr = context.Users.Where(n => n.UserName == "jola17").Select(n => n.Id).FirstOrDefault();
            Message m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "czeœæ";
            m.Date = new DateTime(2016, 11, 20, 10, 10, 10);
            m.Read = true;
            context.Messages.AddOrUpdate(p => p.Date, m);
            context.SaveChanges();

            ids = context.Users.Where(n => n.UserName == "jola17").Select(n => n.Id).FirstOrDefault();
            idr = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "no czeœæ";
            m.Date = new DateTime(2016, 11, 20, 10, 10, 15);
            m.Read = true;
            context.Messages.AddOrUpdate(p => p.Date, m);
            context.SaveChanges();

            ids = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            idr = context.Users.Where(n => n.UserName == "jola17").Select(n => n.Id).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "idziemy na piwo?";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 5);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();

            ids = context.Users.Where(n => n.UserName == "jola17").Select(n => n.Id).FirstOrDefault();
            idr = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "za 15 minut na murkach";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 17);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();


            ids = context.Users.Where(n => n.UserName == "witek15").Select(n => n.Id).FirstOrDefault();
            idr = context.Users.Where(n => n.UserName == "jola17").Select(n => n.Id).FirstOrDefault();
            m = new Message();
            m.SenderID = ids;
            m.ReceiverID = idr;
            m.Text = "elo";
            m.Read = true;
            m.Date = new DateTime(2016, 11, 20, 10, 11, 29);
            context.Messages.AddOrUpdate(m);
            context.SaveChanges();








            Category c = new Category();
            c.Name = "Motoring";
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            int fatherid = context.Categories.Where(p => p.Name == "Motoring").Select(p => p.CategoryID).First();
            c = new Category();
            c.Name = "Cars";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Auto parts";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Car auto equipmnet";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();


            fatherid = context.Categories.Where(p => p.Name == "Cars").Select(p => p.CategoryID).First();
            c = new Category();
            c.Name = "Audi";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Fiat";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Mercedes";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();


            fatherid = context.Categories.Where(p => p.Name == "Auto parts").Select(p => p.CategoryID).First();
            c = new Category();
            c.Name = "Personal";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Vans and Trucks";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();


            c = new Category();
            c.Name = "Music and Education";
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            fatherid = context.Categories.Where(p => p.Name == "Music and Education").Select(p => p.CategoryID).First();
            c = new Category();
            c.Name = "Books";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "Music";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            fatherid = context.Categories.Where(p => p.Name == "Books").Select(p => p.CategoryID).First();
            c = new Category();
            c.Name = "Literature";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();

            c = new Category();
            c.Name = "For children";
            c.CategoryFatherID = fatherid;
            context.Categories.AddOrUpdate(p => p.Name, c);
            context.SaveChanges();





            Models.Attribute a = new Models.Attribute();
            a.Name = "fuel";
            a.Type = "string";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Engine power";
            a.Type = "double";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Engine capacity";
            a.Type = "double";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Body type";
            a.Type = "string";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Transmission";
            a.Type = "string";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Country of origin";
            a.Type = "string";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Mileage";
            a.Type = "double";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Year";
            a.Type = "int";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();

            a = new Models.Attribute();
            a.Name = "Color";
            a.Type = "string";
            context.Attributes.AddOrUpdate(p => p.Name, a);
            context.SaveChanges();







            int aid = context.Attributes.Where(p => p.Name == "fuel").Select(p => p.AttributeID).First();
            AttributeValue av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "gasoline";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "diesel";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "LPG";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "CNG and hybrid";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();


            aid = context.Attributes.Where(p => p.Name == "Body type").Select(p => p.AttributeID).First();
            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Cabriolet";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Sedan";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Coupe";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Pickup";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Hatchback";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Kombi";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "All-terrain";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Minibus";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Minivan";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "SUV";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();


            aid = context.Attributes.Where(p => p.Name == "Transmission").Select(p => p.AttributeID).First();
            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Manual";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Automatic";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();


            aid = context.Attributes.Where(p => p.Name == "Color").Select(p => p.AttributeID).First();
            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "White";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Black";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Gray";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Silver";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Blue";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Brown-Beige";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Red";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Green";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Yellow-Gold";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Different";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();


            aid = context.Attributes.Where(p => p.Name == "Country of origin").Select(p => p.AttributeID).First();
            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Poland";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Germany";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Belarus";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Netherlands";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Sweden";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "USA";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "United Kingdom";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            av = new AttributeValue();
            av.AttributeID = aid;
            av.Value = "Different";
            context.AttributeValues.AddOrUpdate(p => p.Value, av);
            context.SaveChanges();

            int cid = context.Categories.Where(p => p.Name == "Cars").Select(p => p.CategoryID).First();
            aid = context.Attributes.Where(p => p.Name == "fuel").Select(p => p.AttributeID).First();
            CategoryAttribute ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Engine power").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Engine capacity").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Body type").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Transmission").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Country of origin").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Mileage").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Year").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Color").Select(p => p.AttributeID).First();
            ca = new CategoryAttribute();
            ca.CategoryID = cid;
            ca.AttributeID = aid;
            context.CategoryAttributes.AddOrUpdate(p => new { p.AttributeID, p.CategoryID }, ca);
            context.SaveChanges();


            Location l = new Location();
            l.Name = "Bia³ystok";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Bia³ystok, Dziesiêciny";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Bia³ystok, Centrum";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Bia³ystok, Piasta";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Bia³ystok, Bema";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa, Mokotów";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa, Kabaty";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa, M³ociny";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa, Stare Miasto";
            context.Locations.AddOrUpdate(p => p.Name, l);

            l = new Location();
            l.Name = "Warszawa, Œródmieœcie";
            context.Locations.AddOrUpdate(p => p.Name, l);


            string uid = context.Users.Where(p => p.UserName == "slightom").Select(p => p.Id).First();
            cid = context.Categories.Where(p => p.Name == "Audi").Select(p => p.CategoryID).First();
            Classified cl = new Classified();
            cl.UserID = uid;
            cl.Name = "Audi 80 2.0 benzyna";
            cl.Description = "Mam do sprzedania elegancki samochód Audi 80 rocznik 95, silnik 2.0 w benzynie.";
            cl.CategoryID = cid;
            cl.DateAdded = new DateTime(2016, 12, 12, 13, 45, 00);
            cl.Counter = 12;
            cl.Reported = "";
            cl.Price = 6500;
            cl.State = "Used";
            context.Classifieds.AddOrUpdate(p => p.Name, cl);
            context.SaveChanges();

            int clid = context.Classifieds.Where(p => p.Name == "Audi 80 2.0 benzyna").Select(p => p.ClassifiedID).First();
            aid = context.Attributes.Where(p => p.Name == "fuel").Select(p => p.AttributeID).First();
            ClassifiedAttribute cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "gasoline";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Engine power").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "90";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Engine capacity").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "2000";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Body type").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "Kombi";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Transmission").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "Manual";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Country of origin").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "Poland";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Mileage").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "275000";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Year").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "1995";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();

            aid = context.Attributes.Where(p => p.Name == "Color").Select(p => p.AttributeID).First();
            cla = new ClassifiedAttribute();
            cla.ClassifiedID = clid;
            cla.AttributeID = aid;
            cla.Value = "Different";
            context.ClassifiedAttributes.AddOrUpdate(p => new { p.ClassifiedID, p.AttributeID }, cla);
            context.SaveChanges();






            int lid = context.Locations.Where(p => p.Name == "Bia³ystok, Dziesiêciny").Select(p => p.LocationID).First();
            ClassifiedLocation cll = new ClassifiedLocation();
            cll.ClassifiedID = clid;
            cll.LocationID = lid;
            context.ClassifiedLocations.AddOrUpdate(p => new { p.LocationID, p.ClassifiedID }, cll);
            context.SaveChanges();






            PersonalizedCategory pc = new PersonalizedCategory();
            cid = context.Categories.Where(p => p.Name == "Cars").Select(p => p.CategoryID).First();
            pc.UserID = uid;
            pc.PriceMin = 5000;
            pc.PriceMax = 10000;
            pc.CategoryID = cid;
            context.PersonalizedCategories.AddOrUpdate(p => new { p.UserID, p.CategoryID }, pc);
            context.SaveChanges();




            int pcid = context.PersonalizedCategories.Where(p => p.Category.Name == "Cars").Select(p => p.PersonalizedCategoryID).First();
            lid = context.Locations.Where(p => p.Name == "Bia³ystok").Select(p => p.LocationID).First();
            PCL pcl = new PCL();
            pcl.PersonalizedCategoryID = pcid;
            pcl.LocationID = lid;
            context.PCLs.AddOrUpdate(p => new { p.PersonalizedCategoryID, p.LocationID }, pcl);
            context.SaveChanges();





            Photo ph = new Photo();
            ph.ClassifiedID = clid;
            ph.MainPhoto = true;
            ph.Path = "/Content/photos/audi80_main.jpg";
            context.Photos.AddOrUpdate(p => p.Path, ph);
            context.SaveChanges();

            ph = new Photo();
            ph.ClassifiedID = clid;
            ph.MainPhoto = false;
            ph.Path = "/Content/photos/audi80_1.jpg";
            context.Photos.AddOrUpdate(p => p.Path, ph);
            context.SaveChanges();

            ph = new Photo();
            ph.ClassifiedID = clid;
            ph.MainPhoto = false;
            ph.Path = "/Content/photos/audi80_2.jpg";
            context.Photos.AddOrUpdate(p => p.Path, ph);
            context.SaveChanges();

            ph = new Photo();
            ph.ClassifiedID = clid;
            ph.MainPhoto = false;
            ph.Path = "/Content/photos/audi80_3.jpg";
            context.Photos.AddOrUpdate(p => p.Path, ph);
            context.SaveChanges();
        }
    }
}
