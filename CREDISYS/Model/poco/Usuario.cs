using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREDISYS.Model.poco
{
    public class Usuario
    {
        private string username;
        private string password;
        private string nombre;
        private Rol rol;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        internal Rol Rol { get => rol; set => rol = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Usuario(string username, string password, string nombre)
        {
            this.username = username;
            this.password = password;
            this.nombre = nombre;
        }

        
    }
}
