
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
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

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
            txtUsername.Text = "Charlyssde";
            txtPassword.Password = "secret";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            string password = txtPassword.Password;
            byte[] theBytes = Encoding.UTF8.GetBytes(password);


            if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("No puede quedar ningún elemento vacío", "Advertencia");
            }
            else
            {
               
                using (DBEntities db = new DBEntities())
                {

                    try
                    {
                        var user = db.Usuarios.Where(b => b.username.Equals(username) && b.password == theBytes).FirstOrDefault();
                        if (user != null)
                        {
                            DashboardAdmin dashboard_Admin = new DashboardAdmin(user);
                            dashboard_Admin.WindowStartupLocation = this.WindowStartupLocation;
                            dashboard_Admin.Show();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo iniciar sesión", "WARNING");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error a la hora de conectar a la base de datos\n" +
                            "Intente de nuevo más tarde");
                    }

                }
                   
                   
                
            }
            
        }
    }
}
