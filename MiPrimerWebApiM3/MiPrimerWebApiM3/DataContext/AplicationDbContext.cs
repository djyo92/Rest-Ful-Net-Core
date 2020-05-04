using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.DataContext
{
    public class AplicationDbContext : IdentityDbContext<AplicationUser>
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //var roleAdmin = new IdentityRole()
            //{ Id = "89086180-b978-4f90-9dbd-a7040bc93f41", Name = "admin", NormalizedName = "admin" };

            //builder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(builder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("Host=localhost;Database=my_db;Username=my_user;Password=my_pw");

        public DbSet<Autor> Autores  {get;set;}
        public DbSet<Libro> Libros { get; set; }
        public DbSet<HostedServiceLog> HostedServiceLogs { get; set; }
    }
}
