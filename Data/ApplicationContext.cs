using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Configuration;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data
{
    public class ApplicationContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<tblStandard> tblStandard { get; set; }
        public DbSet<tblSubject> tblSubject { get; set; }
        public DbSet<tblAdmissions> tblAdmissions { get; set; }
        public DbSet<tblAdmissionFees> tblAdmissionFees { get; set; }
        public DbSet<tblBlogs> tblBlogs { get; set; }
        public DbSet<tblDownloadCategory> tblDownloadCategory { get; set; }
        public DbSet<tblDownloads> tblDownloads { get; set; }
        public DbSet<tblFaculties> tblFaculties { get; set; }
        public DbSet<tblStudyMaterialCategories> tblStudyMaterialCategories { get; set; }
        public DbSet<tblStudyMaterialFiles> tblStudyMaterialFiles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //}
    }
}
