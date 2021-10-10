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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);            
        //    modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //}
    }
}
