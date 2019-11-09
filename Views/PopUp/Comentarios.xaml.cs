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
    /// Lógica de interacción para Comentarios.xaml
    /// </summary>
    public partial class Comentarios : Window
    {
        EncuestaSolicitud previous;
        public Comentarios(EncuestaSolicitud encuestaSolicitud)
        {
            InitializeComponent();
            this.previous = encuestaSolicitud;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            previous.comentarios = txtComentarios.Text;
            this.Close();
        }
    }
}
