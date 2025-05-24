using FirstEFCoreIntro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEFCoreIntro
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        private readonly string _connectionString;

        public AppDbContext()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

