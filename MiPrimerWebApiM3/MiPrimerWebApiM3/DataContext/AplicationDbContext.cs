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
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("Host=localhost;Database=my_db;Username=my_user;Password=my_pw");

        public DbSet<Autor> Autores  {get;set;}
        public DbSet<Libro> Libros { get; set; }
        public DbSet<HostedServiceLog> HostedServiceLogs { get; set; }
    }
}
