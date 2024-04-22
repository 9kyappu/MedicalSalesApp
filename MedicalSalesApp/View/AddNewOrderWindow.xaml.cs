using MedicalSalesApp.ViewModel;
using System.Windows;

namespace MedicalSalesApp.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        public AddNewOrderWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
