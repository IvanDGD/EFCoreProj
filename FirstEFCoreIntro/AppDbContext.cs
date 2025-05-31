using FirstEFCoreIntro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
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
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Passport> Passports { get; set; }

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .Property(g => g.Name)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .Property(s => s.Scholarship)
                .HasColumnType("decimal(6,2)");

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Passport)
                .WithOne(p => p.Student)
                .HasForeignKey<Passport>(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasCheckConstraint("CK_Student_EmailFormat", "[Email] LIKE '_%@_%._%'");

            modelBuilder.Entity<Passport>()
                .Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(9);

            modelBuilder.Entity<Passport>()
                .HasCheckConstraint("CK_Passport_NumberDigits", "LEN([Number]) = 9 AND [Number] NOT LIKE '%[^0-9]%'");

            modelBuilder.Entity<Teacher>()
                .Property(t => t.FullName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasColumnType("decimal(8,2)")
                .HasDefaultValue(25000);

            modelBuilder.Entity<Teacher>()
                .HasCheckConstraint("CK_Teacher_SalaryPositive", "[Salary] > 0");

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithMany(s => s.Teachers);

            modelBuilder.Entity<Subject>()
                .Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .Property(s => s.Description)
                .IsRequired(false);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}

