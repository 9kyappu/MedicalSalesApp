using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using MedicalSalesApp.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace ManageStaffDBApp.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Motherland { get; set; }
        public decimal Price { get; set; }

        public int SupplinerId { get; set; }
        public virtual Supplier Supplier { get; set; }


        public ICollection<Order> Orders { get; set; }
    }
}
