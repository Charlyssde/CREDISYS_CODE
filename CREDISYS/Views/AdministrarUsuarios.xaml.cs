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
        public AdministrarUsuarios()
        {
            InitializeComponent();
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                using (DBEntities db = new DBEntities())
                {
                    roles = db.Rols.ToList<Rol>();
                }
                EditarUsuario screen = new EditarUsuario(selected, roles);
                screen.ShowDialog();
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
            screen.WindowStartupLocation = this.WindowStartupLocation;
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
                    txtNombre.Text = "";
                    txtUsername.Text = "";
                    txtRol.Text = "";
                    txtPassword.Password = "";
                }
                else
                {
                    try
                    {
                        selected = db.Usuarios.Where(b => b.username.Equals(username)).FirstOrDefault();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                    if (selected != null)
                    {
                        txtNombre.Text = selected.nombre;
                        txtUsername.Text = selected.username;
                        txtRol.Text = selected.Rol.rol1;
                        txtPassword.Password = "" + selected.password;

                        btnDelete.IsEnabled = true;
                        btnEdit.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                    }
                }
            }
            txtBusqueda.Text = "";
        }
 
    private void limpiarInformacion()
        {
            selected = null;
            txtNombre.Text = "";
            txtPassword.Password = "";
            txtUsername.Text = "";
            txtRol.Text = "";
            
        }
    }

   
}
