using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dashboardArduinoApp.Clases
{
    internal class ClaseArduino
    {
        private SerialPort Arduino;
        private String puertoUSB = "COM5";
        private Int32 puertoSerial = 9600;
        private Int32 tiempoEspera = 10000;
        private String registro = "";

        private Double senTemperatura;
        private Double senHumedad;
        private Double gasSen;
        private Double senVoltaje;
        private Double senFotorresistencia;

        public ClaseArduino(string? puertoUSB, int puertoSerial, int tiempoEspera)
        {
            this.Arduino1 = new SerialPort();
            this.puertoUSB = puertoUSB;
            this.puertoSerial = puertoSerial;
            this.tiempoEspera = tiempoEspera;
        }

        public ClaseArduino()
        {
            this.Arduino1 = new SerialPort();
        }

        public bool InicializarConexionArduino()
        {

            if (!Arduino1.IsOpen)
            {
                try
                {
                    Arduino1.PortName = this.puertoUSB;
                    Arduino1.BaudRate = this.puertoSerial;
                    Arduino1.ReadTimeout = this.tiempoEspera;

                    Arduino1.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            else
            {
                return true;
            }
        }

        public string LecturaSerial()
        {
            if (Arduino1.IsOpen) {
                try
                {
                    string x = Arduino.ReadExisting();
                    return x;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return "Error";
        } 

        public bool CerrarPuertoArduino()
        {
            if (Arduino1.IsOpen)
            {
                Arduino1.Close();
                return true;
            }
            
            return false;

        }

        public string DistribuirLecturas(string x) 
        {
            x.Trim();
            x = x.Replace(" ", "");
            string[] lecturas = x.Split(",");

            SenTemperatura = Convert.ToDouble(lecturas[1]);
            SenHumedad = Convert.ToDouble(lecturas[2]);
            SenVoltaje = Convert.ToDouble(lecturas[3]);
            GasSen = Convert.ToDouble(lecturas[4]);
            SenFotorresistencia = Convert.ToDouble(lecturas[5]);

            return string.Concat(SenTemperatura, senHumedad, senFotorresistencia, senVoltaje , gasSen);
        }

        public void InsertarRegidstrosDB()
        {
            string query;
            query = $"INSERT INTO registros (Fecha, FechaHora, Temperatura, Humedad, Voltaje, Gas, Fotorresistencia) " +
                $"VALUES (curdate(), now(), {SenTemperatura}, {SenHumedad}, {SenVoltaje}, {GasSen}, {SenFotorresistencia});";

            ClaseConexion.EjecutarQuery(query);
        }


        public string? PuertoUSB { get => puertoUSB; set => puertoUSB = value; }
        public int PuertoSerial { get => puertoSerial; set => puertoSerial = value; }
        public int TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }
        public string Registro { get => registro; set => registro = value; }
        public SerialPort Arduino1 { get => Arduino; set => Arduino = value; }

        public double SenTemperatura { get => senTemperatura; set => senTemperatura = value; }
        public double SenHumedad { get => senHumedad; set => senHumedad = value; }
        public double SenVoltaje { get => senVoltaje; set => senVoltaje = value; }
        public double SenFotorresistencia { get => senFotorresistencia; set => senFotorresistencia = value; }
        public double GasSen { get => gasSen; set => gasSen = value; }
    }
}
