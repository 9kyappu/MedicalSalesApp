using MedicalSalesApp.Model.Data;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSalesApp.Model
{
    public static class DataWorker
    {
        //получить всех покупателей
        public static List<Customer> GetAllCustomers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Customers.ToList();
                return result;
            }
        }
        //получить всех изготовителей
        public static List<Supplier> GetAllSuppliers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Suppliers.ToList();
                return result;
            }
        }
        //получить все заказы
        public static List<Order> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.ToList();
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
        //получить все препараты в заказах
        public static List<MedicineOrder> GetAllMedicineOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.MedicineOrders.ToList();
                return result;
            }
        }
        //создать покупателя
        public static string CreateCustomer(string full_name, string address, string phone, string email)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли покупатель
                bool checkIsExist = db.Customers.Any(el => el.Full_name == full_name && el.Phone == phone);
                if (!checkIsExist)
                {
                    Customer newCustomer = new Customer 
                    { 
                        Full_name = full_name,
                        Address = address,
                        Phone = phone,
                        Email = email
                    };
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //содать препараты в заказе
        public static string CreateMedicineOrder(Medicine medicine, Order order)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                MedicineOrder newMedicineOrder = new MedicineOrder
                {                       
                    OrderId = order.Id,
                    MedicineId = medicine.Id,
                };
                db.MedicineOrders.Add(newMedicineOrder);
                db.SaveChanges();
                string result = "Сделано!";
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
        //создать заказ
        public static string CreateOrder(string status, DateTime date, Customer customer)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Order newOrder = new Order
                {
                    Status = status,
                    Date = date,
                    CustomerId = customer.Id
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();
                string result = "Сделано!";
                return result;
            }
        }
        //удаление покупателя
        public static string DeleteCustomer(Customer customer)
        {
            string result = "Такого покупателя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                result = "Сделано! Покупатель " + customer.Full_name + " удален";
            }
            return result;
        }
        //удаление препарата в заказе
        public static string DeleteMedicineOrder(MedicineOrder medicineOrder)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check position is exist
                db.MedicineOrders.Remove(medicineOrder);
                db.SaveChanges();
                result = "Сделано! Позиция удалена";
            }
            return result;
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
        //удаление заказа
        public static string DeleteOrder(Order order)
        {
            string result = "Такого заказа не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                result = "Сделано! Заказ удалён";
            }
            return result;
        }
        //редактирование покупателя
        public static string EditCustomer(Customer oldCustomer, string newFull_name, string newAddress, string newPhone, string newEmail)
        {
            string result = "Такого покупателя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.Id == oldCustomer.Id);
                customer.Full_name = newFull_name;
                customer.Address = newAddress;
                customer.Phone = newPhone;
                customer.Email = newEmail;
                db.SaveChanges();
                result = "Сделано! Покупатель " + customer.Full_name + " изменен";
            }
            return result;
        }

        //редактирование препарата
        public static string EditMedicine(Medicine oldMedicine, string newName, string newDosage, string newMotherland, decimal newPrice, Supplier newSupplier)
        {
            string result = "Такого препарата не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check medicine is exist
                Medicine medicine = db.Medicines.FirstOrDefault(m => m.Id == oldMedicine.Id);
                if (medicine != null)
                {
                    medicine.Name = newName;
                    medicine.Dosage = newDosage;
                    medicine.Motherland = newMotherland;
                    medicine.Price = newPrice;
                    medicine.SupplierId = newSupplier.Id;
                    db.SaveChanges();
                    result = "Сделано! препарат " + medicine.Name + " изменен";
                }
            }
            return result;
        }

        //редактирование изготовителя
        public static string EditSupplier(Supplier oldSupplier, string newName, string newPhone, string newAddress, string newEmail)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check Supplier is exist
                Supplier supplier = db.Suppliers.FirstOrDefault(s => s.Id == oldSupplier.Id);
                if (supplier != null)
                {
                    supplier.Name = newName;
                    supplier.Phone = newPhone;
                    supplier.Address = newAddress;
                    supplier.Email = newEmail;
                    db.SaveChanges();
                    result = "Сделано! Сотрудник " + supplier.Name + " изменен";
                }
            }
            return result;
        }

        //редактирование заказа
        public static string EditOrder(Order oldOrder, string newStatus, DateTime newDate, Customer newCustomer)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check order is exist
                Order order = db.Orders.FirstOrDefault(o => o.Id == oldOrder.Id);
                if (order != null)
                {
                    order.Status = newStatus;
                    order.Date = newDate;
                    order.CustomerId = newCustomer.Id;
                    db.SaveChanges();
                    result = "Сделано! Заказ №" + order.Id + " изменен";
                }
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

        // получение клиента по id клиента
        public static Customer GetCustomerById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.Id == id);
                return customer;
            }
        }

        // получение заказа по id заказа
        public static Order GetOrderById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Order order = db.Orders.FirstOrDefault(o => o.Id == id);
                return order;
            }
        }
        // получение препарата по id препарата
        public static Medicine GetMedicineById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Medicine medicine = db.Medicines.FirstOrDefault(m => m.Id == id);
                return medicine;
            }
        }

        // получение всех заказов по id покупателя
        public static List<Order> GetAllOrdersByCustomerId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllOrders() where order.CustomerId == id select order).ToList();
                return orders;
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
