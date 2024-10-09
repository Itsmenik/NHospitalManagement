

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hospital.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Repositery
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

         [NotMapped]
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts {get; set; }
        public DbSet<Lab> Labs { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineReport> MedicineReport { get; set; }
        public DbSet<Payroll> Payroll {get; set; }  
        public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; } 
        public DbSet<Room> Rooms { get; set; } 
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<TestPrice> TestPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Doctor) // Assuming a navigation property
                .WithMany() // Depending on the relationships defined
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete for DoctorId

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Patient) // Assuming a navigation property
                .WithMany() // Depending on the relationships defined
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction); // No cascading delete for PatientId
        }

    }
}
 
