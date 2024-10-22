using Core.Entities.Concrete;
using Entities.Common;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = A302ArchitectureAppDb; Trusted_Connection = True; TrustServerCertificate = True;");
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public override int SaveChanges()
        {
            //var entities = ChangeTracker.Entries()
            //    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
            //    .Select(x => x.Entity)
            //    .OfType<BaseEntity>();

            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return base.SaveChanges();
        }

        
    }
}
