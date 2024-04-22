using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSalesApp.Model
{
    public class MedicineOrder
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
