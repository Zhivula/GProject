using GraduationProject.DataBase;
using OxyPlot;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GraduationProject.ViewModel
{
    class AnalysisViewModel : INotifyPropertyChanged
    {
        private float max;
        private float min;
        private int count;
        private float step;

        public float Min
        {
            get => min;
            set
            {
                min = value;
                OnPropertyChanged(nameof(Min));
            }
        }
        public float Max
        {
            get => max;
            set
            {
                max = value;
                OnPropertyChanged(nameof(Max));
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public float Step
        {
            get => step;
            set
            {
                step = value;
                OnPropertyChanged(nameof(Step));
            }
        }

        private PlotModel plot;
        public PlotModel Plot
        {
            get => plot;
            set
            {
                plot = value;
                OnPropertyChanged(nameof(Plot));
            }
        }
        public AnalysisViewModel()
        {
            Plot = GetPlotModel();
        }
        public PlotModel GetPlotModel()
        {
            var plot = new PlotModel();

            var line = new OxyPlot.Series.LineSeries()
            {
                Title = "Зависимость потерь электроэнергии от потока электроэнергии в головном участке в абсолютных единицах",
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 2,
                MarkerSize = 1,
                MarkerType = MarkerType.Circle
            };

            for (int i = 0; i < Count; i++)
            {
                line.Points.Add(new DataPoint(Min + Step*i, 5));
            }

            plot.Series.Add(line);
            plot.TextColor = OxyColors.White;
            plot.PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
            plot.PlotAreaBorderColor = OxyColors.White;

            var dateAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Transparent, IntervalLength = 80, Maximum = Max, Minimum = Min };
            plot.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Gray, IntervalLength = 30 };
            plot.Axes.Add(valueAxis);

            return plot;
        }

        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChangeFirst.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChangeFirst.Children[i].GetType().Name;
                if (childTypeName == "AnalysisView")
                {
                    window.GridChangeFirst.Children.RemoveAt(i);
                }
            }
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
