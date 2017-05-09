using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SSFAPP.DAL.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SSFAPP.DAL
{
    public class SSFContext : DbContext
    {
        public SSFContext() : base("SSFDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<SSFContext>(new InitializeDb());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fish>().HasRequired(f => f.CaughtByUser).WithMany(u => u.FishList);
            

            modelBuilder.Entity<Topic>().HasRequired(t => t.WrittenByUser).WithMany(c => c.topics);

            modelBuilder.Entity<Comment>().HasRequired(f => f.WrittenByUser).WithMany(u => u.CommentList);

            modelBuilder.Entity<Comment>().HasRequired(f => f.Topic).WithMany(u => u.Comments);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}