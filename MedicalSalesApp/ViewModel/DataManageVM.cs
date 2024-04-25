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
                NotifyPropertyChanged("AllSuppliers");
            }
        }



        //свойства для препаратов
        public static string MedicineName { get; set; }
        public static string MedicineDosage { get; set; }
        public static string MedicineMotherland { get; set; }
        public static decimal MedicinePrice { get; set; }
        public static Supplier MedicineSupplier { get; set; }

        //свойства для изготовителей
        public static string SupplierName { get; set; }
        public static string SupplierPhone { get; set; }
        public static string SupplierAddress { get; set; }
        public static string SupplierEmail { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static User SelectedUser { get; set; }
        public static Medicine SelectedMedicine { get; set; }
        public static Supplier SelectedSupplier { get; set; }


        #region COMMANDS TO ADD

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
 
        #endregion

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    
                    //если препарат
                    if (SelectedTabItem.Name == "MedicinesTab" && SelectedMedicine != null)
                    {
                        resultStr = DataWorker.DeleteMedicine(SelectedMedicine);
                        UpdateAllDataView();
                    }                   
                    //если изготовитель
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
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void OpenAddMedicineWindowMethod()
        {
            AddNewMedicineWindow newMedicineWindow = new AddNewMedicineWindow();
            SetCenterPositionAndOpen(newMedicineWindow);
        }

        private void OpenAddSupplierWindowMethod()
        {
            AddNewSupplierWindow newSupplierWindow = new AddNewSupplierWindow();
            SetCenterPositionAndOpen(newSupplierWindow);
        }


        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            //для препарата
            MedicineName = null;
            MedicineDosage = null;
            MedicineMotherland = null;
            MedicinePrice = 0;
            MedicineSupplier = null;
            // для изготовителя
            SupplierName = null;
            SupplierPhone = null;
            SupplierAddress = null;
            SupplierEmail = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllMedicineView();
            UpdateAllSuppliersView();
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