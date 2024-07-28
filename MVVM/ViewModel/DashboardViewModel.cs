using dashboardArduinoApp.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using MaterialDesignColors.Recommended;
using System.Windows.Controls;

namespace dashboardArduinoApp.MVVM.ViewModel
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly List<DateTimePoint> _Temperatura = new();
        private readonly List<DateTimePoint> _Voltaje = new();
        private readonly List<DateTimePoint> _Humedad = new();
        private readonly List<DateTimePoint> _Gas = new();
        private readonly List<DateTimePoint> _Luz = new();
        private readonly DateTimeAxis _customAxis;

        public DashboardViewModel()
        {
            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _Temperatura,
                    Fill = null,
                    Name = "Temperatura",
                    Stroke = new LinearGradientPaint(new[]{ new SKColor(45, 64, 89), new SKColor(255, 212, 96)}) { StrokeThickness = 10 },
                    GeometryStroke = new LinearGradientPaint(new[]{ new SKColor(45, 64, 89), new SKColor(255, 212, 96)}) { StrokeThickness = 5 }
                },
                new LineSeries<DateTimePoint>
                {
                    Values = _Voltaje,
                    Fill = null,
                    Name = "Voltaje"
                    
                }
            };

            Series2 = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _Humedad,
                    Fill = null,
                    Name = "Humedad"
                },
                new LineSeries<DateTimePoint>
                {
                    Values = _Gas,
                    Fill = null,
                    Name = "Gas"
                },
                new LineSeries<DateTimePoint>
                {
                    Values = _Luz,
                    Fill = null,
                    Name = "Luz"
                }
            };

            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new Axis[] { _customAxis };

            _ = ReadData();

        }

        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<ISeries> Series2 { get; set; }

        public Axis[] XAxes { get; set; }

        public Axis[] YAxes { get; set; } =
    {
        new Axis
        {
            Labeler = value => $"{value} %" ,
        }
    };

        public object Sync { get; } = new object();

        public bool IsReading { get; set; } = true;







        private async Task ReadData()
        {
            // to keep this sample simple, we run the next infinite loop 
            // in a real application you should stop the loop/task when the view is disposed 

            while (IsReading)
            {
                await Task.Delay(2000);

                // Because we are updating the chart from a different thread 
                // we need to use a lock to access the chart data. 
                // this is not necessary if your changes are made in the UI thread. 
                SensorTemperatura.SelectRegistro();
                SensorVoltaje.SelectRegistro();
                SensorHumedad.SelectRegistro();
                SensorGas.SelectRegistro();
                SensorFotorresistencia.SelectRegistro();

                lock (Sync)
                {
                    _Temperatura.Add(new DateTimePoint(DateTime.Now, SensorTemperatura.Registro));
                    _Voltaje.Add(new DateTimePoint(DateTime.Now, SensorVoltaje.Registro));
                    _Humedad.Add(new DateTimePoint(DateTime.Now, SensorHumedad.Registro));
                    _Luz.Add(new DateTimePoint(DateTime.Now, SensorFotorresistencia.Registro));
                    _Gas.Add(new DateTimePoint(DateTime.Now, SensorGas.Registro));

                    if (_Temperatura.Count > 15) { 
                        _Temperatura.RemoveAt(0);
                        _Voltaje.RemoveAt(0);
                        _Gas.RemoveAt(0);
                        _Humedad.RemoveAt(0);
                        _Luz.RemoveAt(0);
                    }

                    // we need to update the separators every time we add a new point 
                    _customAxis.CustomSeparators = GetSeparators();
                }
            }
        }

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
            };
        }

        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds;

            return secsAgo < 1
                ? "Ahora"
                : $"{secsAgo:N0}s";
        }
    }
}
