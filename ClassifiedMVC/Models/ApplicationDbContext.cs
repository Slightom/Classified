using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("ClassifiedDB", throwIfV1Schema: false) { }


        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Classified> Classifieds { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<ClassifiedAttribute> ClassifiedAttributes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }


        public DbSet<PersonalizedCategory> PersonalizedCategories { get; set; }
        public DbSet<PCL> PCLs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ClassifiedLocation> ClassifiedLocations { get; set; }
        public DbSet<Photo> Photos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // to hierahical structure of Categories
            modelBuilder.Entity<Category>()
                        .HasOptional(c => c.CategoryFather)
                        .WithMany()
                        .HasForeignKey(cf => cf.CategoryFatherID);

            // Messages relations
            modelBuilder.Entity<Message>()
                        .HasRequired(b => b.Sender)
                        .WithMany(a => a.MessagesSent)
                        .HasForeignKey(b => b.SenderID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                        .HasRequired(b => b.Receiver)
                        .WithMany(a => a.MessagesReceived)
                        .HasForeignKey(b => b.ReceiverID)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ClassifiedMVC.Models.AllAtributesModel> AllAtributesModels { get; set; }
        public System.Data.Entity.DbSet<ClassifiedMVC.Models.UploadModel> UploadModels { get; set; }
    }
}