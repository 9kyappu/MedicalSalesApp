using MedicalSalesApp.Model;
using MedicalSalesApp.View;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MedicalSalesApp.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        //все покупатели
        private List<Customer> allCustomers = DataWorker.GetAllCustomers();
        public List<Customer> AllCustomers
        {
            get { return allCustomers; }
            set
            {
                allCustomers = value;
                NotifyPropertyChanged("AllCustomers");
            }
        }

        //все Препараты
        private List<Medicine> allMedicines = DataWorker.GetAllMedicines();
        public List<Medicine> AllMedicines
        {
            get
            {
                return allMedicines;
            }
            private set
            {
                allMedicines = value;
                NotifyPropertyChanged("AllMedicines");
            }
        }
        ////все препараты в заказе
        //private List<User> allUsers = DataWorker.GetAllUsers();
        //public List<User> AllUsers
        //{
        //    get
        //    {
        //        return allUsers;
        //    }
        //    private set
        //    {
        //        allUsers = value;
        //        NotifyPropertyChanged("AllUsers");
        //    }
        //}

        //все заказы
        private List<Order> allOrders = DataWorker.GetAllOrders();
        public List<Order> AllOrders
        {
            get
            {
                return allOrders;
            }
            private set
            {
                allOrders = value;
                NotifyPropertyChanged("AllOrders");
            }
        }
        //все изготовители
        private List<Supplier> allSuppliers = DataWorker.GetAllSuppliers();
        public List<Supplier> AllSuppliers
        {
            get
            {
                return allSuppliers;
            }
            private set
            {
                allSuppliers = value;
                NotifyPropertyChanged("AllSupplier");
            }
        }

        //свойства для покупателя
        public static string CustomerFull_name { get; set; }
        public static string CustomerAddress { get; set; }
        public static string CustomerPhone { get; set; }
        public static string CustomerEmail { get; set; }

        //свойства для препаратов в заказе
        public static Order MedicineOrderOrder { get; set; }
        public static Medicine MedicineOrderMedicine { get; set; }

        //свойства для препаратов
        public static string MedicineName { get; set; }
        public static string MedicineDosage { get; set; }
        public static string MedicineMotherland { get; set; }
        public static decimal MedicinePrice { get; set; }
        public static Supplier MedicineSupplier { get; set; }
        //свойства для заказов
        public static string OrderStatus { get; set; }
        public static Customer OrderCustomer { get; set; }
        public static DateTime OrderDate { get; set; }
        //свойства для изготовителей
        public static string SupplierName { get; set; }
        public static string SupplierPhone { get; set; }
        public static string SupplierAddress { get; set; }
        public static string SupplierEmail { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static User SelectedUser { get; set; }
        public static Customer SelectedCustomer { get; set; }
        public static Medicine SelectedMedicine { get; set; }
        public static MedicineOrder SelectedMedicineOrder { get; set; }
        public static Order SelectedOrder { get; set; }
        public static Supplier SelectedSupplier { get; set; }


        #region COMMANDS TO ADD
        private RelayCommand addNewCustomer;
        public RelayCommand AddNewCustomer
        {
            get
            {
                return addNewCustomer ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "всё топ";
                    if (CustomerFull_name == null || CustomerFull_name.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Full_nameBlock");
                    }
                    if (CustomerAddress == null || CustomerAddress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "AddressBlock");
                    }
                    if (CustomerPhone == null || CustomerPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PhoneBlock");
                    }
                    if (CustomerEmail == null || CustomerEmail.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "EmailBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateCustomer(CustomerFull_name, CustomerAddress, CustomerPhone, CustomerEmail);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewMedicineOrder;
        public RelayCommand AddNewMedicineOrder
        {
            get
            {
                return addNewMedicineOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "всё топ";
                    if (MedicineOrderOrder == null)
                    {
                        MessageBox.Show("Выберете номер заказа");
                    }
                    if (MedicineOrderMedicine == null)
                    {
                        MessageBox.Show("Выберете препарат");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateMedicineOrder(MedicineOrderMedicine, MedicineOrderOrder);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewMedicine;
        public RelayCommand AddNewMedicine
        {
            get
            {
                return addNewMedicine ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "всё топ";
                    if (MedicineName == null || MedicineName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (MedicineDosage == null || MedicineDosage.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "DosageBlock");
                    }
                    if (MedicineMotherland == null || MedicineMotherland.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "MotherlandBlock");
                    }
                    if (MedicinePrice == 0)
                    {
                        SetRedBlockControll(wnd, "PriceBlock");
                    }
                    if (MedicineSupplier == null)
                    {
                        MessageBox.Show("Выберете изготовителя");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateMedicine(MedicineName, MedicineDosage, MedicineMotherland, MedicinePrice, MedicineSupplier);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewSupplier;
        public RelayCommand AddNewSupplier
        {
            get
            {
                return addNewSupplier ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "всё топ";
                    if (SupplierName == null || SupplierName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (SupplierAddress == null || SupplierAddress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "AddressBlock");
                    }
                    if (SupplierPhone == null || SupplierPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PhoneBlock");
                    }
                    if (SupplierEmail == null || SupplierEmail.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "EmailBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateSupplier(SupplierName, SupplierPhone, SupplierAddress, SupplierEmail);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }
        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "всё топ";
                    if (OrderStatus == null)
                    {
                        MessageBox.Show("Выберете статус заказа");
                    }
                    if (OrderCustomer == null)
                    {
                        MessageBox.Show("Выберете покупателя");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateOrder(OrderStatus, OrderDate, OrderCustomer);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        #endregion

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если покупатель
                    if (SelectedTabItem.Name == "CustomersTab" && SelectedCustomer != null)
                    {
                        resultStr = DataWorker.DeleteCustomer(SelectedCustomer);
                        UpdateAllDataView();
                    }
                    //если препарат
                    if (SelectedTabItem.Name == "MedicinesTab" && SelectedMedicine != null)
                    {
                        resultStr = DataWorker.DeleteMedicine(SelectedMedicine);
                        UpdateAllDataView();
                    }
                    //если заказ
                    if (SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        resultStr = DataWorker.DeleteOrder(SelectedOrder);
                        UpdateAllDataView();
                    }                    
                    //если препарат в заказе
                    if (SelectedTabItem.Name == "MedicineOrdersTab" && SelectedMedicineOrder != null)
                    {
                        resultStr = DataWorker.DeleteMedicineOrder(SelectedMedicineOrder);
                        UpdateAllDataView();
                    }                    
                    //если изготовительOpenAddNewDepartmentWnd
                    if (SelectedTabItem.Name == "SuppliersTab" && SelectedSupplier != null)
                    {
                        resultStr = DataWorker.DeleteSupplier(SelectedSupplier);
                        UpdateAllDataView();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                );
            }
        }

        #region EDIT COMMANDS
        private RelayCommand editCustomer;
        public RelayCommand EditCustomer
        {
            get
            {
                return editCustomer ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран клиент";
                    string noPositionStr = "Не выбран новый адрес";
                    if (SelectedCustomer != null)
                    {
                        if (CustomerAddress != null)
                        {
                            resultStr = DataWorker.EditCustomer(SelectedCustomer, CustomerFull_name, CustomerAddress, CustomerPhone, CustomerEmail);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand editMedicine;
        public RelayCommand EditMedicine
        {
            get
            {
                return editMedicine ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран клиент";
                    string noPositionStr = "Не выбран новый адрес";
                    if (SelectedMedicine != null)
                    {
                        if (MedicineName != null)
                        {
                            // Call a method to edit the medicine using the Medicine class
                            resultStr = DataWorker.EditMedicine(SelectedMedicine, MedicineName, MedicineDosage, MedicineMotherland, MedicinePrice, MedicineSupplier);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }


        private RelayCommand editOrder;
        public RelayCommand EditOrder
        {
            get
            {
                return editOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    if (SelectedOrder != null)
                    {
                        resultStr = DataWorker.EditOrder(SelectedOrder, OrderStatus, OrderDate, OrderCustomer);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        private RelayCommand editSupplier;
        public RelayCommand EditSupplier
        {
            get
            {
                return editSupplier ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";
                    if (SelectedSupplier != null)
                    {
                        resultStr = DataWorker.EditSupplier(SelectedSupplier, SupplierName, SupplierPhone, SupplierAddress, SupplierEmail);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openAddNewCustomerWnd;
        public RelayCommand OpenAddNewCustomerWnd
        {
            get
            {
                return openAddNewCustomerWnd ?? new RelayCommand(obj =>
                {
                    OpenAddCustomerWindowMethod();
                }
                    );
            }
        }
        private RelayCommand openAddNewSupplierWnd;
        public RelayCommand OpenAddNewSupplierWnd
        {
            get
            {
                return openAddNewSupplierWnd ?? new RelayCommand(obj =>
                {
                    OpenAddSupplierWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewMedicineOrderWnd;
        public RelayCommand OpenAddNewMedicineOrderWnd
        {
            get
            {
                return openAddNewMedicineOrderWnd ?? new RelayCommand(obj =>
                {
                    OpenAddMedicineOrderWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewMedicineWnd;
        public RelayCommand OpenAddNewMedicineWnd
        {
            get
            {
                return openAddNewMedicineWnd ?? new RelayCommand(obj =>
                {
                    OpenAddMedicineWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewOrderWnd;
        public RelayCommand OpenAddNewOrderWnd
        {
            get
            {
                return openAddNewOrderWnd ?? new RelayCommand(obj =>
                {
                    OpenAddOrderWindowMethod();
                }
                );
            }
        }
        private RelayCommand openEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если покупатель
                    if (SelectedTabItem.Name == "CustomersTab" && SelectedCustomer != null)
                    {
                        OpenEditCustomerWindowMethod(SelectedCustomer);
                    }
                    //если препарат
                    if (SelectedTabItem.Name == "MedicinesTab" && SelectedMedicine != null)
                    {
                        OpenEditMedicineWindowMethod(SelectedMedicine);
                    }
                    //если заказ
                    if (SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        OpenEditOrderWindowMethod(SelectedOrder);
                    }                   
                    //если изготовитель
                    if (SelectedTabItem.Name == "SuppliersTab" && SelectedSupplier != null)
                    {
                        OpenEditSupplierWindowMethod(SelectedSupplier);
                    }
                }
                    );
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOW
        //методы открытия окон
        private void OpenAddCustomerWindowMethod()
        {
            AddNewCustomerWindow newCustomerWindow = new AddNewCustomerWindow();
            SetCenterPositionAndOpen(newCustomerWindow);
        }
        private void OpenAddMedicineWindowMethod()
        {
            AddNewMedicineWindow newMedicineWindow = new AddNewMedicineWindow();
            SetCenterPositionAndOpen(newMedicineWindow);
        }

        private void OpenAddMedicineOrderWindowMethod()
        {
            AddNewMedicineOrderWindow newMedicineOrderWindow = new AddNewMedicineOrderWindow();
            SetCenterPositionAndOpen(newMedicineOrderWindow);
        }
        private void OpenAddOrderWindowMethod()
        {
            AddNewOrderWindow newOrderWindow = new AddNewOrderWindow();
            SetCenterPositionAndOpen(newOrderWindow);
        }
        private void OpenAddSupplierWindowMethod()
        {
            AddNewSupplierWindow newSupplierWindow = new AddNewSupplierWindow();
            SetCenterPositionAndOpen(newSupplierWindow);
        }

        //окна редактирования
        private void OpenEditCustomerWindowMethod(Customer customer)
        {
            EditCustomerWindow editCustomerWindow = new EditCustomerWindow();
            SetCenterPositionAndOpen(editCustomerWindow);
        }

        private void OpenEditMedicineWindowMethod(Medicine medicine)
        {
            EditMedicineWindow editMedicineWindow = new EditMedicineWindow();
            SetCenterPositionAndOpen(editMedicineWindow);
        }

        //private void OpenEditMedicineOrderWindowMethod()
        //{
        //    EditMedicineOrderWindow editMedicineOrderWindow = new EditMedicineOrderWindow();
        //    SetCenterPositionAndOpen(editMedicineOrderWindow);
        //}

        private void OpenEditOrderWindowMethod(Order order)
        {
            EditOrderWindow editOrderWindow = new EditOrderWindow();
            SetCenterPositionAndOpen(editOrderWindow);
        }

        private void OpenEditSupplierWindowMethod(Supplier supplier)
        {
            EditSupplierWindow editSupplierWindow = new EditSupplierWindow();
            SetCenterPositionAndOpen(editSupplierWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //для покупателя
            CustomerFull_name = null;
            CustomerAddress = null;
            CustomerPhone = null;
            CustomerEmail = null;
            //для препарата в заказе
            MedicineOrderOrder = null;
            MedicineOrderMedicine = null;
            //для препарата
            MedicineName = null;
            MedicineDosage = null;
            MedicineMotherland = null;
            MedicinePrice = 0;
            MedicineSupplier = null;
            // для заказа
            OrderStatus = null;
            OrderCustomer = null;
            OrderDate = new DateTime();
            // для изготовителя
            SupplierName = null;
            SupplierPhone = null;
            SupplierAddress = null;
            SupplierEmail = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllCustomersView();
            UpdateAllMedicineView();
            UpdateAllSuppliersView();
            UpdateAllOrdersView();
        }

        private void UpdateAllCustomersView()
        {
            AllCustomers = DataWorker.GetAllCustomers();
            MainWindow.AllCustomersView.ItemsSource = null;
            MainWindow.AllCustomersView.Items.Clear();
            MainWindow.AllCustomersView.ItemsSource = AllCustomers;
            MainWindow.AllCustomersView.Items.Refresh();
        }
        private void UpdateAllMedicineView()
        {
            AllMedicines = DataWorker.GetAllMedicines();
            MainWindow.AllMedicinesView.ItemsSource = null;
            MainWindow.AllMedicinesView.Items.Clear();
            MainWindow.AllMedicinesView.ItemsSource = AllMedicines;
            MainWindow.AllMedicinesView.Items.Refresh();
        }
        private void UpdateAllSuppliersView()
        {
            AllSuppliers = DataWorker.GetAllSuppliers();
            MainWindow.AllSuppliersView.ItemsSource = null;
            MainWindow.AllSuppliersView.Items.Clear();
            MainWindow.AllSuppliersView.ItemsSource = AllSuppliers;
            MainWindow.AllSuppliersView.Items.Refresh();
        }
        private void UpdateAllOrdersView()
        {
            AllOrders = DataWorker.GetAllOrders();
            MainWindow.AllOrdersView.ItemsSource = null;
            MainWindow.AllOrdersView.Items.Clear();
            MainWindow.AllOrdersView.ItemsSource = AllOrders;
            MainWindow.AllOrdersView.Items.Refresh();
        }
        //private void UpdateAllMedicineOrdersView()
        //{
        //    AllMedi = DataWorker.GetAllSuppliers();
        //    MainWindow.AllSuppliersView.ItemsSource = null;
        //    MainWindow.AllSuppliersView.Items.Clear();
        //    MainWindow.AllSuppliersView.ItemsSource = AllSuppliers;
        //    MainWindow.AllSuppliersView.Items.Refresh();
        //}

        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
