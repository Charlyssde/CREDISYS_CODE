using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREDISYS.Model.poco
{
    class Ciudad
    {
        private int id;
        private string ciudadtex;
        private int idEstado;
                
        public int Id { get => id; set => id = value; }
        public string Ciudadtex { get => ciudadtex; set => ciudadtex = value; }
        public int IdEstado { get => idEstado; set => idEstado = value; }

        public Ciudad(int id, string ciudadtex, int idEstado)
        {
            this.id = id;
            this.ciudadtex = ciudadtex;
            this.idEstado = idEstado;
        }
    }

}
