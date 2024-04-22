using MedicalSalesApp.Model;
using Microsoft.EntityFrameworkCore;

namespace MedicalSalesApp.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MedicineOrder> MedicineOrders { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MedicalSalesAppDB2;Trusted_Connection=True;");
        }
    }
}
