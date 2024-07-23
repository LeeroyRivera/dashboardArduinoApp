using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts;
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
using static dashboardArduinoApp.MainWindow;

namespace dashboardArduinoApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<MeasureModel>()
            .X(x => x.Value)
            .Y(x => x.Value);

            //save the mapper globally         
            Charting.For<MeasureModel>(mapper);

            a = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<MeasureModel>()
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
        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

    }
}
