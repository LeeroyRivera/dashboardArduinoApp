using dashboardArduinoApp.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dashboardArduinoApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for Arduino.xaml
    /// </summary>
    public partial class Arduino : UserControl
    {
        static ClaseArduino ObjetoArduino;

        public Arduino()
        {
            InitializeComponent();


            Task.Factory.StartNew(() => {
                int x = 1;
                while (this.IsInitialized)
                {
                    lock (Globales.regsitro)
                    {
                        escrituraSerial(Globales.regsitro, x);
                    }
                    x++;
                    Thread.Sleep(2000);
                }
            });
            //escrituraSerial(ObjetoArduino.Registro);
        }

        private void escrituraSerial(string x, int y)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                tbxArduino.Text = $"Registro No.{y}: " + x;

            }));
        }
    }
}
