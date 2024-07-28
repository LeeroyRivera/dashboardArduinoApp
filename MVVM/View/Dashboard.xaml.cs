
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
using Google.Protobuf.WellKnownTypes;
using System.ComponentModel;
using System.Diagnostics;
using dashboardArduinoApp.Clases;

namespace dashboardArduinoApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl, INotifyPropertyChanged
    {

        public Dashboard()
        {
            InitializeComponent();
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
    public class MeasureModel
    {
        public DateTime ElapsedMilliseconds { get; set; }
        public double Value { get; set; }
    }
}
