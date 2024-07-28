using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dashboardArduinoApp.Clases
{
    static class SensorFotorresistencia
    {
        private static DateTime tiempo;
        private static double registro;

        public static DateTime Tiempo { get => tiempo; set => tiempo = value; }
        public static double Registro { get => registro; set => registro = value; }

        public static void SelectRegistro()
        {
            string query = "select FechaHora, Fotorresistencia from registros order by FechaHora desc limit 1;";

            DataTable dt = ClaseConexion.Query(query);

            if (dt.Rows.Count > 0)
            {
                Tiempo = (DateTime)dt.Rows[0]["FechaHora"];
                Registro = (double)Convert.ToDecimal(dt.Rows[0]["Fotorresistencia"].ToString());
            }
        }
    }
}
