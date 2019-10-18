using CREDISYS.Model.db;
using CREDISYS.Model.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREDISYS.Model.dao
{
    class RolDAO
    {
        public static Rol getRol(int id)
        {
            Rol rol = null;
            SqlConnection conn = ConnectionDB.getConnection();

            if (conn != null)
            {
                String comm = "Select * from dbo.Rol where idRol = '" + id + "' ;";
                SqlCommand sqlCommand = new SqlCommand(comm, conn);
                SqlDataReader dr;
                dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    int idRol = dr.GetInt32(0);
                    String nombre = dr.GetString(1);
                    rol = new Rol(idRol, nombre);

                }
                sqlCommand.Dispose();
                dr.Close();
            }

            return rol;
        }
    }
}
