using CREDISYS.Model.db;
using CREDISYS.Model.poco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CREDISYS.Model.dao
{
    class CiudadDAO
    {
        public static Ciudad getCiudad(int id)
        {
            Ciudad ciudad = null;
            SqlConnection conn = ConnectionDB.getConnection();

            if (conn != null)
            {
                String comm = "Select * from dbo.Ciudad where idCiudad = '" + id + "' ;";
                SqlCommand sqlCommand = new SqlCommand(comm, conn);
                SqlDataReader dr;
                dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    int idCiudad = dr.GetInt32(0);
                    String nombre = dr.GetString(1);
                    int idEstado = dr.GetInt32(2);
                    ciudad = new Ciudad(idCiudad, nombre, idEstado);

                }
                sqlCommand.Dispose();
                dr.Close();
            }

            return ciudad;

        }
        public void agregarCiudad(Ciudad ciudad)
        {
            if (ciudad != null)
            {
                SqlConnection conn = ConnectionDB.getConnection();

                if (conn != null)
                {
                    String comm = "INSERT INTO CIUDAD (IDCIUDAD, CIUDAD, IDESTADO)" + "VALUES ('" + ciudad.Id + "', '" + ciudad.Ciudadtex +
                        "', '" + ciudad.IdEstado + "')";
                    SqlCommand sqlCommand = new SqlCommand(comm, conn);
                    sqlCommand.CommandType = CommandType.Text;
                    conn.Open();
                    try
                    {
                        int i = sqlCommand.ExecuteNonQuery();
                        if (i > 0)
                            MessageBox.Show("Ciudad registrada");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                    finally
                    {
                        // Cierro la Conexión.
                        conn.Close();
                    }
                }
            }
        }


        public void actualizarCiudad(Ciudad ciudad)
        {
            if (ciudad != null)
            {
                SqlConnection conn = ConnectionDB.getConnection();

                if (conn != null)
                {
                    string sql = "UPDATE POSTRES SET Ciudad='" + ciudad.Ciudadtex + "' WHERE idCiudad=" + ciudad.Id + "";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                            MessageBox.Show("Ciudad actualizada correctamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                    finally
                    {
                        // Cierro la Conexión.
                        conn.Close();
                    }
                }
            }
        }
    }
}
