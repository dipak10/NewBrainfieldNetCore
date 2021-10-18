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
        public DbSet<tblVideos> tblVideos { get; set; }
        public DbSet<tblGallery> tblGallery { get; set; }
        public DbSet<tblTestimonials> tblTestimonials { get; set; }
        public DbSet<tblExamCategory> tblExamCategory { get; set; }
        public DbSet<tblChapters> tblChapters { get; set; }
        public DbSet<tblExamMaster> tblExamMaster { get; set; }
        public DbSet<tblExamSubject> tblExamSubject { get; set; }
        public DbSet<tblPackageMaster> tblPackageMaster { get; set; }
        public DbSet<tblPackageExam> tblPackageExam { get; set; }
        public DbSet<tblUserCart> tblUserCart { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //}
    }
}
