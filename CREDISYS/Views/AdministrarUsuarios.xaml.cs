using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AdministrarUsuarios : Window
    {
        public AdministrarUsuarios()
        {
            InitializeComponent();
            
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                String username = txtBusqueda.Text;
                
                List<Usuario> users = db.Usuarios.Where(b => b.username.Equals(username)).ToList<Usuario>();
                dg_Datos.ItemsSource = users;
                dg_Datos.Columns.RemoveAt(dg_Datos.Columns.Count - 1);
                dg_Datos.Columns.RemoveAt(dg_Datos.Columns.Count - 1);

            }
        }

        private void dg_Datos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }
    }
}
