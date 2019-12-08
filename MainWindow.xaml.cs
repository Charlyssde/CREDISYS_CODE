
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
using CREDISYS.Properties;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            /*
             * TEMPORAL PARA PRUEBAS
             */
            //txtUsername.Text = "Charlyssde";
            txtPassword.Password = "secret";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            string password = txtPassword.Password;
            byte[] theBytes = Encoding.UTF8.GetBytes(password);


            if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
               /**
                * Consulta para encontrar el usuario
                */
                using (DBEntities db = new DBEntities())
                {

                    try
                    {
                        var user = db.Usuarios.Where(b => b.username.Equals(username) && b.password == theBytes).FirstOrDefault();
                        if (user != null)
                        {
                            /**
                             * De acuerdo al rol, entra al apartado del sistema correspondiente
                             */
                            switch (user.idRol)
                            {
                                case 5:
                                    DashboardAdmin dashboard_Admin = new DashboardAdmin(user);
                                    dashboard_Admin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                    dashboard_Admin.Show();
                                    closeWindow();
                                    break;
                                case 6:
                                    Dashboard_Capturista dashboard_Capturista = new Dashboard_Capturista();
                                    dashboard_Capturista.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                    dashboard_Capturista.Show();
                                    closeWindow();
                                    break;
                                case 7:
                                    
                                    Dashboard_Gestor dashboard_Gestor = new Dashboard_Gestor();
                                    dashboard_Gestor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                    dashboard_Gestor.Show();
                                    closeWindow();
                                    break;
                                case 8:
                                    Dashboard_AnalistaC dashboard_AnalistaC = new Dashboard_AnalistaC(user);
                                    dashboard_AnalistaC.WindowStartupLocation = this.WindowStartupLocation;
                                    dashboard_AnalistaC.Show();
                                    closeWindow();
                                    break;
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }

                }
                   
                   
                
            }
            
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
