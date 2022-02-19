using GraduationProject.Data;
using GraduationProject.DataBase;
using GraduationProject.Model;
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
                Max = Min + (Count-1) * Step;
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
                Max = Min + (Count - 1) * Step;
            }
        }
        public float Step
        {
            get => step;
            set
            {
                step = value;
                OnPropertyChanged(nameof(Step));
                Max = Min + (Count - 1) * Step;
            }
        }
        private List<Losses> items;
        public List<Losses> ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }
        private PlotModel plotdW;
        public PlotModel PlotdW
        {
            get => plotdW;
            set
            {
                plotdW = value;
                OnPropertyChanged(nameof(PlotdW));
            }
        }
        private PlotModel plotdWxx;
        public PlotModel PlotdWxx
        {
            get => plotdWxx;
            set
            {
                plotdWxx = value;
                OnPropertyChanged(nameof(PlotdWxx));
            }
        }
        private PlotModel plotdWnt;
        public PlotModel PlotdWnt
        {
            get => plotdWnt;
            set
            {
                plotdWnt = value;
                OnPropertyChanged(nameof(PlotdWnt));
            }
        }
        private PlotModel plotdWnl;
        public PlotModel PlotdWnl
        {
            get => plotdWnl;
            set
            {
                plotdWnl = value;
                OnPropertyChanged(nameof(PlotdWnl));
            }
        }
        public AnalysisViewModel()
        {
            ItemsSource = new List<Losses>();
        }
        public List<PlotModel> GetPlotModel()
        {
            var plotList = new List<PlotModel>();

            var linedWxx = new OxyPlot.Series.LineSeries()
            {
                Title = "Потери холостого хода в головном участке в абсолютных единицах",
                Color = OxyColor.FromRgb(22, 186, 124),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };

            var linedWnt = new OxyPlot.Series.LineSeries()
            {
                Title = "Нагрузочные потери трансформаторов в головном участке в абсолютных единицах",
                Color = OxyColor.FromRgb(24, 6, 189),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            var linedWnl = new OxyPlot.Series.LineSeries()
            {
                Title = "Нагрузочные потери линий в головном участке в абсолютных единицах",
                Color = OxyColor.FromRgb(189, 33, 6),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            var linedW = new OxyPlot.Series.LineSeries()
            {
                Title = "Суммарные потери в головном участке в абсолютных единицах",
                Color = OxyColor.FromRgb(2, 191, 212),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle
            };
            for (int i = 0; i < Count; i++)
            {
                ChangedKz(Min + Step * i);

                var dWxx = GetdWxx();
                var dWnt = GetdWnt();
                var dWnl = GetdWnl();
                var dW = dWxx + dWnt + dWnl;
                var root = GlobalGrid.GetInstance().Tree.Root;

                if (root != null)
                { 
                    var firstLine = root.View.DataContext as ButtonViewModel;

                    ItemsSource.Add(new Losses()
                    {
                        Kz = (Min + Step * i).ToString(),
                        dWxx = dWxx.ToString("#.##"),
                        dWxxPercent = ((dWxx / firstLine.Wp1) * 100).ToString("#.##"),
                        dWnt = dWnt.ToString("#.##"),
                        dWntPercent = ((dWnt / firstLine.Wp1)*100).ToString("#.##"),
                        dWnl = dWnl.ToString("#.##"),
                        dWnlPercent = ((dWnl / firstLine.Wp1)*100).ToString("#.##"),
                        dW = dW.ToString("#.##"),
                        dWPercent = ((dW / firstLine.Wp1)*100).ToString("#.##")
                    });

                    linedWxx.Points.Add(new DataPoint(Min + Step * i, dWxx));
                    linedWnt.Points.Add(new DataPoint(Min + Step * i, dWnt));
                    linedWnl.Points.Add(new DataPoint(Min + Step * i, dWnl));
                    linedW.Points.Add(new DataPoint(Min + Step * i, dW));
                }
            }

            OnPropertyChanged(nameof(ItemsSource));

            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());
            plotList.Add(new PlotModel());

            plotList[2].Series.Add(linedWnt);
            plotList[3].Series.Add(linedWnl);
            plotList[0].Series.Add(linedW);
            plotList[1].Series.Add(linedWxx);

            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, MajorGridlineColor = OxyColors.Gray};

            foreach (var i in plotList)
            {
                i.TextColor = OxyColors.Gray;
                i.PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
                i.PlotAreaBorderColor = OxyColors.White;
                //i.Axes.Add(valueAxis);
            }

            return plotList;
        }
        public void ChangedKz(double newKz)
        {
            var global = GlobalGrid.GetInstance();
            global.Tree.ChangedKz(newKz);
        }
        public double GetdWxx()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWxx().Values.Sum();
            
            return list;
        }
        public double GetdWnt()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWnt().Values.Sum();

            return list;
        }
        public double GetdWnl()
        {
            var global = GlobalGrid.GetInstance();

            var list = global.Tree.GetdWnl().Values.Sum();

            return list;
        }
        public ICommand Calculate => new DelegateCommand(o =>
        {
            var list = GetPlotModel();

            PlotdW = list[0];
            PlotdWxx = list[1];
            PlotdWnt = list[2];
            PlotdWnl = list[3];
        });

        /// <summary>
        /// Построение и анализ зависимости потерь электроэнергии в абсолютных и относительных единицах в функции отпуска электроэнергии
        /// </summary>
        public ICommand Paragraph_3 => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.FullGridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.FullGridChange.Children[i].GetType().Name;
                if (childTypeName == "AnalysisView")
                {
                    window.FullGridChange.Children.RemoveAt(i);
                }
            }
        });
        /// <summary>
        /// Построение и анализ зависимости стоимости передачи электроэнергии от отпуска электроэнергии
        /// </summary>
        public ICommand Paragraph_4 => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.FullGridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.FullGridChange.Children[i].GetType().Name;
                if (childTypeName == "AnalysisView")
                {
                    window.FullGridChange.Children.RemoveAt(i);
                }
            }
        });
        public ICommand Return => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.FullGridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.FullGridChange.Children[i].GetType().Name;
                if (childTypeName == "AnalysisView")
                {
                    window.FullGridChange.Children.RemoveAt(i);
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
