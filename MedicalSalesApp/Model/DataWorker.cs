using MedicalSalesApp.Model.Data;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSalesApp.Model
{
    public static class DataWorker
    {
        //получить всех изготовителей
        public static List<Supplier> GetAllSuppliers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Suppliers.ToList();
                return result;
            }
        }
        //получить все препараты
        public static List<Medicine> GetAllMedicines()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Medicines.ToList();
                return result;
            }
        }
        //создать препарат
        public static string CreateMedicine(string name, string dosage, string motherland, decimal price, Supplier supplier)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the medicine is exist
                bool checkIsExist = db.Medicines.Any(el => el.Name == name && el.Dosage == dosage && el.Motherland == motherland);
                if (!checkIsExist)
                {
                    Medicine newMedicine = new Medicine
                    {
                        Name = name,
                        Dosage = dosage,
                        Motherland = motherland,
                        Price = price,
                        SupplierId = supplier.Id
                    };
                    db.Medicines.Add(newMedicine);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //создать изготовителя
        public static string CreateSupplier(string name, string phone, string address, string email)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the Supplier is exist
                bool checkIsExist = db.Suppliers.Any(el => el.Name == name && el.Phone == phone && el.Address == address);
                if (!checkIsExist)
                {
                    Supplier newSupplier = new Supplier
                    {
                        Name = name,
                        Phone = phone,
                        Address = address,
                        Email = email
                    };
                    db.Suppliers.Add(newSupplier);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //удаление препарата
        public static string DeleteMedicine(Medicine medicine)
        {
            string result = "Такого препарата не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Medicines.Remove(medicine);
                db.SaveChanges();
                result = "Сделано! Препарат " + medicine.Name + " удалён";
            }
            return result;
        }        
        //удаление изготовителя
        public static string DeleteSupplier(Supplier supplier)
        {
            string result = "Такого изготовителя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
                result = "Сделано! Изготовитель " + supplier.Name + " удалён";
            }
            return result;
        }
 
        // получение поставщика по id поставщика
        public static Supplier GetSupplierById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Supplier supplier = db.Suppliers.FirstOrDefault(s => s.Id == id);
                return supplier;
            }
        }

        // получение всех медикаментов по id изготовителя
        public static List<Medicine> GetAllMediciansBySupplierId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Medicine> medicians = (from medicine in GetAllMedicines() where medicine.SupplierId == id select medicine).ToList();
                return medicians;
            }
        }
    }
}
