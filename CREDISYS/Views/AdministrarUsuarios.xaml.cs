using CREDISYS.Properties;
using CREDISYS.Views.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AdministrarUsuarios : Window
    {
        Usuario selected = null;
        List<Rol> roles;

        Usuario usuario;

        DashboardAdmin dashboardAdmin;

        public AdministrarUsuarios(Usuario usuario, DashboardAdmin dashboardAdmin)
        {
            InitializeComponent();
            cargarInfo(usuario, dashboardAdmin);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DashboardAdmin dashboardAdmin = new DashboardAdmin(this.usuario);
            dashboardAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dashboardAdmin.Show();
            closeWindow();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                using (DBEntities db = new DBEntities())
                {
                    roles = db.Rols.ToList<Rol>();
                }
                EditarUsuario editarUsuario = new EditarUsuario(selected, roles);
                editarUsuario.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                editarUsuario.ShowDialog();
            }
            limpiarInformacion();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result =  MessageBox.Show("¿Desea eliminar?", "Confirmación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (selected != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.Usuarios.Remove(selected);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                }
            }
            limpiarInformacion();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario screen = new RegistrarUsuario();
            screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            screen.ShowDialog();
            limpiarInformacion();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            
            using (DBEntities db = new DBEntities())
            {
                String username = txtBusqueda.Text;

                if (username.Equals("")) 
                {
                    MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    limpiarInformacion();
                }
                else
                {
                    try
                    {
                        selected = db.Usuarios.Where(b => b.username.Equals(username)).FirstOrDefault();
                        if (selected != null)
                        {
                            txtNombre.Text = selected.nombre;
                            txtUsername.Text = selected.username;
                            txtRol.Text = selected.Rol.rol1;
                            txtPassword.Password = "" + selected.password;

                            if (selected.username == this.usuario.username)
                            {
                                MessageBox.Show("Este usuario no se puede editar o eliminar... Porque eres tú");
                            }
                            else
                            {
                                btnDelete.IsEnabled = true;
                                btnEdit.IsEnabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                    
                }
            }
            txtBusqueda.Text = "";
        }

        private void cargarInfo(Usuario usuario, DashboardAdmin dashboardAdmin)
        {
            this.usuario = usuario;
            this.dashboardAdmin = dashboardAdmin;
        }

        private void limpiarInformacion()
        {
            selected = null;

            this.btnDelete.IsEnabled = false;
            this.btnEdit.IsEnabled = false;

            txtNombre.Text = "";
            txtPassword.Password = "";
            txtUsername.Text = "";
            txtRol.Text = "";
            
        }

        private void closeWindow()
        {
            this.Close();
        }
    }

   
}
