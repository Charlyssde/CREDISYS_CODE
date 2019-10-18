using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREDISYS.Model.poco
{
    public class Rol
    {
        private int id;
        private string rol;

        public Rol(int id, string rol)
        {
            this.id = id;
            this.rol = rol;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => rol; set => rol = value; }
    }
}
