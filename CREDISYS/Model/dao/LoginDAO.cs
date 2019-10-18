using CREDISYS.Model.db;
using CREDISYS.Model.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CREDISYS.Model.dao
{
    class LoginDAO
    {
        public static Usuario Login(String username, String password)
        {
            Usuario login = null;
            SqlConnection conn = null;
            String command = "select * from dbo.Usuario where username = '" + username + "' and password = '" + password + "';";
            conn = ConnectionDB.getConnection();
            if (conn != null)
            {
                SqlCommand request = new SqlCommand(command, conn);

                SqlDataReader rd;
                rd = request.ExecuteReader();
                int id = 0;
                if (rd.Read())
                {
                    String nombre = rd.GetString(2);
                    login = new Usuario(username, password, nombre);
                    id = rd.GetInt32(3);
                }
                request.Dispose();
                rd.Close();
                if (id == 0)
                {
                    MessageBox.Show("Error al accesar", "WARNING");
                }
                else
                {
                    Rol rol = RolDAO.getRol(id);
                    if (rol.Equals(null))
                    {
                        return null;
                    }
                    else
                    {
                        login.Rol = rol;
                    }
                    
                }
                conn.Close();
                
            }
            return login;
        }
    }
}
