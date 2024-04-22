using MedicalSalesApp.ViewModel;
using System.Windows;

namespace MedicalSalesApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewCustomerWindow.xaml
    /// </summary>
    public partial class AddNewCustomerWindow : Window
    {
        public AddNewCustomerWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
