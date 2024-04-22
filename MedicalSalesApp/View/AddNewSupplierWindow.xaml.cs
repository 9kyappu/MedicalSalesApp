using MedicalSalesApp.ViewModel;
using System.Windows;

namespace MedicalSalesApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewSupplierWindow.xaml
    /// </summary>
    public partial class AddNewSupplierWindow : Window
    {
        public AddNewSupplierWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
