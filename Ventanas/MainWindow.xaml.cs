using dashboardArduinoApp.Clases;
using System;
using System.IO.Ports;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace dashboardArduinoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Brush defaultBrushBtnSalir;
        private Brush defaultBrushBtnMinimizar;
        StringBuilder sb = new StringBuilder();

        char LF = (char)10;

        private ClaseArduino Arduino;

        public MainWindow()
        {
            InitializeComponent();

            Arduino = ClaseArduino.GetClaseArduino();
            ClaseConexion.conectar();

            Arduino.InicializarConexionArduino();
            Arduino.Arduino1.DataReceived += Arduino_DataReceived;
           // PruebaArduino();
           // escrituraSerial();

            defaultBrushBtnSalir = BtnSalir.Background;
            defaultBrushBtnMinimizar = BtnMinimizar.Background;
        }

        private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (Arduino.InicializarConexionArduino())
            {
                try
                {
                    string Data = Arduino.LecturaSerial();
                    Globales.regsitro = Data;
                            char LF = 'y';
                            foreach (char c in Data)
                            {
                                if (c == LF)
                                {
                                    sb.Append(c);
                                    string currentLine = sb.ToString();
                                    sb.Clear();

                                    Arduino.DistribuirLecturas(currentLine);
                                    Arduino.InsertarRegidstrosDB();
                                    //double x = Convert.ToDouble(Arduino.DistribuirLecturas(CurrentLine));
                                }
                                else if (Regex.IsMatch(c.ToString(), @"[0-9.,]"))
                                {
                                    sb.Append(c);
                                }
                            }         
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al ejecutar la tarea" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Conecte el arduino");
            }
            /*if (Arduino.Arduino1.IsOpen)
            {
                try
                {
                    Task.Factory.StartNew(() =>
                    {
                        //ReadLine() i think only returns the port's buffer if there's a '\n' on the very end. if it's in
                        //the middle... i dont think it gives one and ignores it, returning null.
                        //This whole method might fire 2 or 3 times, and only the last time will it actually add a message... i think.
                        escrituraSerial(Arduino.LecturaSerial());
                    });
                }
                catch (Exception)
                {

                    throw;
                }
            }
            */
        }


        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {  
            startPoint = e.GetPosition(TopBorder);
            TopBorder.CaptureMouse();
            //MessageBox.Show("click");
        }

        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var currentPoint = e.GetPosition(TopBorder);
            if (e.LeftButton == MouseButtonState.Pressed &&
                TopBorder.IsMouseCaptured &&
                (Math.Abs(currentPoint.X - startPoint.X) >  
                    SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(currentPoint.Y - startPoint.Y) >
                    SystemParameters.MinimumVerticalDragDistance))
            {
                TopBorder.ReleaseMouseCapture();
                DragMove();
            }
        }

        private void Button_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           // var converter = new BrushConverter();
           // BtnSalir.Background = (Brush)converter.ConvertFromString("Red");
        }

        private void BtnSalir_MouseEnter(object sender, MouseEventArgs e)
        {
            var converter = new BrushConverter();
            BtnSalir.Background = (Brush)converter.ConvertFromString("#FFED5353");
        }

        private void BtnSalir_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnSalir.Background = defaultBrushBtnSalir;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }

        private void BtnMinimizar_MouseEnter(object sender, MouseEventArgs e)
        {
            var converter = new BrushConverter();
            BtnMinimizar.Background = (Brush)converter.ConvertFromString("#FF313338");
        }

        private void BtnMinimizar_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnMinimizar.Background = defaultBrushBtnSalir;
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }


        private void Window_Closed(object sender, EventArgs e)
        {
           Arduino.CerrarPuertoArduino();
        }

    }
}