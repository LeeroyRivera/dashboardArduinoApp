using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dashboardArduinoApp.Clases
{
    internal class ClaseArduino
    {
        SerialPort Arduino;
        String puertoUSB = "COM5";
        Int32 puertoSerial = 9600;
        Int32 tiempoEspera = 10000;
        String registro = "";
        

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
            Arduino.NewLine = "\r";
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

        public string? PuertoUSB { get => puertoUSB; set => puertoUSB = value; }
        public int PuertoSerial { get => puertoSerial; set => puertoSerial = value; }
        public int TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }
        public string Registro { get => registro; set => registro = value; }
        public SerialPort Arduino1 { get => Arduino; set => Arduino = value; }
    }
}
