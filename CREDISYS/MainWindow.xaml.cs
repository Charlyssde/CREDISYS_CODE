using CREDISYS.Model.dao;
using CREDISYS.Model.poco;
using CREDISYS.Views;
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

namespace CREDISYS
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Password;
            if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("No puede quedar ningún elemento vacío", "Advertencia");
            }
            else
            {
                Usuario user = LoginDAO.Login(username, password);
                if (user == null)
                {
                    MessageBox.Show("No se pudo iniciar sesión", "WARNING");
                }
                else
                {
                    DashboardAdmin dashboard_Admin = new DashboardAdmin(user);
                    dashboard_Admin.WindowStartupLocation = this.WindowStartupLocation;
                    dashboard_Admin.Show();

                    this.Close();
                }
            }
            
        }
    }
}
