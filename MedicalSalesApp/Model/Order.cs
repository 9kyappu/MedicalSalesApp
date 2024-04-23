using MedicalSalesApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSalesApp.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [NotMapped]
        public Customer OrderCustomer
        {
            get
            {
                return DataWorker.GetCustomerById(CustomerId);
            }
        }
    }
}
