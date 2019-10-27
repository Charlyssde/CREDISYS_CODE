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

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario screen = new RegistrarUsuario();
            screen.WindowStartupLocation = this.WindowStartupLocation;
            screen.ShowDialog();
            
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            
            using (DBEntities db = new DBEntities())
            {
                String username = txtBusqueda.Text;

                if (username.Equals("")) 
                {
                    MessageBox.Show("No ha ingresado elementos para su búsqueda");
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
                        MessageBox.Show("Error a la hora de conectar a la base de datos\n" +
                            "Intente de nuevo más tarde");
                    }
                    if (selected != null)
                    {
                        txtNombre.Text = selected.nombre;
                        txtUsername.Text = selected.username;
                        txtRol.Text = selected.Rol.rol1;
                        txtPassword.Password = "" + selected.password;
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado ningún elemento");
                    }
                }
            }
            txtBusqueda.Text = "";
        }

        private void dg_Datos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }
    }
}
