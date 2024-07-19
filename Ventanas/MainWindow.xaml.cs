using dashboardArduinoApp.Clases;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.IO.Ports;
using System.Reflection.Emit;
using System.Text;
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
        SerialPort Arduino; bool ArduinoEnabled = false;

        public MainWindow()
        {
            InitializeComponent();

            defaultBrushBtnSalir = BtnSalir.Background;
            defaultBrushBtnMinimizar = BtnMinimizar.Background;

            Arduino = new SerialPort();
            Arduino.PortName = "COM5";
            Arduino.BaudRate = 9600;
            Arduino.ReadTimeout = 1000;

            a = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                }
            };
            b = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 10, 5, 27, 14 }
                }
            };
            DataContext = this;
        }

        public SeriesCollection a { get; set; }
        public SeriesCollection b { get; set; }


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
            Arduino.Close();
        }



        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {


            //try
            //{
            //    Arduino.Open();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            //string txt = string.Empty;

            //new Thread(() =>
            //{

            //    while (!Arduino.IsOpen)
            //    {
            //        try
            //        {
            //            string txt = Arduino.ReadLine();


            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }
            //    }
            //    Dispatcher.BeginInvoke(() => txtPrueba.Text = txt);
            //}).Start();
        }
    }
}