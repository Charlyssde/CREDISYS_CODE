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
using System.Windows.Shapes;

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {
        List<Rol> roles;
        List<String> listRoles;
        public RegistrarUsuario()
        {
            InitializeComponent();
            cargarRoles();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txtUsername.Text.Equals("") || txtNombre.Text.Equals("") || txtPassword.Password.Equals("")
                        || cbRoles.SelectedItem == null)
                    {
                        MessageBox.Show("Todos los campos deben estar completos");
                    }
                    else
                    {
                        Usuario existe = db.Usuarios.Where(b => b.username.Equals(txtUsername.Text)).FirstOrDefault();
                        if (existe != null)
                        {
                            MessageBox.Show("Ya existe un usuario con ese nombre de usuario, por favor " +
                                "introduzca otro nombre");
                            txtUsername.Text = "";
                        }
                        else
                        {
                            Usuario nuevo = new Usuario();
                            nuevo.nombre = txtNombre.Text;
                            nuevo.username = txtUsername.Text;
                            
                            foreach (Rol rol in roles)
                            {
                                if (rol.rol1.Equals(cbRoles.SelectedItem))
                                {
                                    nuevo.idRol = rol.idRol;
                                }    
                            }
                            byte[] bytes = Encoding.ASCII.GetBytes(txtPassword.Password);
                            nuevo.password = bytes;

                            db.Usuarios.Add(nuevo);
                            db.SaveChanges();
                            MessageBox.Show("Guardado exitósamente en la base de datos");
                            closeWindow();
                        }
                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Error a la hora de conectar a la base de datos\n" +
                            "Intente de nuevo más tarde");
                }

            }
            
        }

        private void closeWindow()
        {
            this.Close();
        }

        private void cargarRoles()
        {
            using (DBEntities db = new DBEntities())
            {

                roles = db.Rols.ToList<Rol>();
                listRoles = new List<String>();

                foreach (Rol r in roles)
                {
                    listRoles.Add(r.rol1);
                }
                cbRoles.ItemsSource = listRoles;

            }

        }
    }
}
