using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Lógica de interacción para EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        private Usuario usuario;
        private List<String> rolesString;
        private List<Rol> roles;

        public EditarUsuario(Usuario usuario, List<Rol> roles)
        {
            InitializeComponent();
            cargarDatos(usuario, roles);
        }

        private void cargarDatos(Usuario usuario, List<Rol> roles)
        {
            this.usuario = usuario;
            txtNombre.Text = this.usuario.nombre;
            txtUsername.Text = this.usuario.username;
            txtPassword.Password = "" + this.usuario.password;
            this.roles = roles;

            rolesString = new List<String>();
            foreach (Rol rol in roles)
            {
                rolesString.Add(rol.rol1);
            }
            cbRoles.ItemsSource = rolesString;

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
                            Usuario usuarioM = db.Usuarios.Where(b => b.username == usuario.username).FirstOrDefault();
                            byte[] bytes = Encoding.ASCII.GetBytes(txtPassword.Password);
                            usuarioM.password = bytes;
                            foreach (Rol rol in roles)
                            {
                                if (rol.rol1.Equals(cbRoles.SelectedItem))
                                {
                                    usuarioM.idRol = rol.idRol;
                               }
                           }
                        db.SaveChanges();

                        MessageBox.Show("Actualización efectuada correctamente");
                        closeWindow();
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
    }
}
