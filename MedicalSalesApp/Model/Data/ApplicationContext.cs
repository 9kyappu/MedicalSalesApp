using MedicalSalesApp.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalSalesApp.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MedicalSalesAppDB;Trusted_Connection=True;");
        }
    }
}
