using MedicalSalesApp.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace ManageStaffDBApp.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Full_name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
