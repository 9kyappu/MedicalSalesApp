using MedicalSalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalSalesApp.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllCustomersView;
        public static ListView AllMedicineOrdersView;
        public static ListView AllOrdersView;
        public static ListView AllMedicinesView;
        public static ListView AllSuppliersView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllCustomersView = ViewAllCustomers;
            AllMedicinesView = ViewAllMedicines;
            AllMedicineOrdersView = ViewAllMedicineOrders;
            AllOrdersView = ViewAllOrders;
            AllSuppliersView = ViewAllSuppliers;
        }
    }
}
