using dashboardArduinoApp.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dashboardArduinoApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjectViews
    {

        private object _currentView;

        public RelayCommand DashboardCommand { get; set; }
        public RelayCommand ArduinoCommand { get; set; }
        public RelayCommand BaseDatosCommand { get; set; }

        public DashboardViewModel DashboardVM { get; set; }
        public ArduinoViewModel ArduinoVM { get; set; }
        public BaseDatosViewModel BaseDatosVM { get; set; }

        public object CurrentView 
        { 
            get
            {
                return _currentView;
            } 
            set
            {
                _currentView = value;
                OnPropertyChanged();
            } 
        }


        public MainViewModel() { 

            DashboardVM = new DashboardViewModel();
            ArduinoVM = new ArduinoViewModel();
            BaseDatosVM = new BaseDatosViewModel();

            CurrentView = DashboardVM;

            DashboardCommand = new RelayCommand(o =>
            {
                CurrentView = DashboardVM;
            });

            ArduinoCommand = new RelayCommand(o =>
            {
                CurrentView = ArduinoVM;
            });

            BaseDatosCommand = new RelayCommand(o =>
            {
                CurrentView = BaseDatosVM;
            });
        }
    }
}
