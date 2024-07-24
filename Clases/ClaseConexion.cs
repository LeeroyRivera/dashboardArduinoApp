using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dashboardArduinoApp.Clases
{
    internal class ClaseConexion
    {

        private class Conexion
        {
            public static string conString = "server=localhost;port=3306;user=UserMicrocon;password=12345;database=proyectosensores";
        }

        public static MySqlConnection conectar()
        {
            MySqlConnection con = new MySqlConnection(Conexion.conString);
            try
            {
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexion de la base de datos: " + ex.Message);
            }
        }

        public static DataTable Query(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlConnection con = conectar();
                var consulta = new MySqlCommand(sql, con);
                var adapter = new MySqlDataAdapter(consulta);
                con.Close();

                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return dt;
            }
        }

        public static Boolean EjecutarQuery(string sql)
        {
            try
            {
                MySqlConnection con = conectar();
                var consulta = new MySqlCommand(sql, con);
                consulta.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
