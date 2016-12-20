using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("ClassifiedDB", throwIfV1Schema: false) { }


        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Classified> Classified { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<ClassifiedAttribute> ClassifiedAttributes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }


        public DbSet<PersonalizedCategory> PersonalizedCategories { get; set; }
        public DbSet<PCL> PCLs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ClassifiedLocation> ClassifiedLocations { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}