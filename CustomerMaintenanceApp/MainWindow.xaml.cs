using CustomerMaintenanceApp.Classes;
using CustomerMaintenanceApp.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomerMaintenanceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerList customerList = new CustomerList();
        public MainWindow()
        {
            InitializeComponent();
            customerList.Changed += CustomerList_Changed;
            customerList.Fill();
            RefreshCustomerList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = new Customer
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text
                };
                customerList += customer;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstCustomers.SelectedItem is Customer selectedCustomer)
            {
                customerList -= selectedCustomer;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            customerList.Save();
            MessageBox.Show("Customers saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CustomerList_Changed(CustomerList customers)
        {
            RefreshCustomerList();
        }

        private void RefreshCustomerList()
        {
            lstCustomers.ItemsSource = null;
            lstCustomers.ItemsSource = customerList;
        }
        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}